using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Un4seen.Bass;
using Un4seen.BassWasapi;
using Un4seen.Bass.AddOn.Midi;
using System.Diagnostics;
using System.Threading;
using System.IO;
using Microsoft.Win32;

namespace MIDITestCSharp
{
    public partial class Main : Form
    {
        static bool isitrunning = true;    // Lel

        static int chan;                   // Channel handle
        static int font;                   // SoundFont

        static int miditempo;              // MIDI file tempo
        static float temposcale = 1.0f;    // Tempo adjustment

        static SYNCPROC LyricSyncProc;
        static SYNCPROC EndSyncProc;
        static SYNCPROC TempoSyncProc;
        static SYNCPROC LoopSyncProc;
        static WASAPIPROC WasapiProc;

        static string lyrics;              // Lyrics buffer

        static bool IsWinXPOrOlder()
        {
            OperatingSystem OS = Environment.OSVersion;
            return (OS.Version.Major <= 5);
        }

        void Error(string es)
        {
            MessageBox.Show(String.Format("{0}\n(error code: {1})", es, Bass.BASS_ErrorGetCode()));
        }

        private void LyricSync(int handle, int channel, int data, IntPtr user)
        {
            try
            {
                BASS_MIDI_MARK mark = new BASS_MIDI_MARK();
                string text;
                string p;
                int lines;
                BassMidi.BASS_MIDI_StreamGetMark(channel, (BASSMIDIMarker)user, data, mark);
                text = mark.text;
                if (text[0] == '@')
                {
                    lyrics = "No lyrics available.";
                    return; // skip info
                }
                if (text[0] == '\\')
                { // clear display
                    lyrics = "" + text.Substring(1);
                    p = lyrics;
                }
                else
                {
                    if (text[0] == '/')
                    { // new line
                        text = '\n' + text.Substring(1);
                    }
                    lyrics += text;
                }

                lines = lyrics.Split('\n').Length;
                if (lines > 3)
                { // remove old lines so that new lines fit in display...
                    int a;
                    for (a = 0; a < lines - 3; a++)
                    {
                        lyrics = lyrics.Substring(lyrics.IndexOf('\n') + 1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void EndSync(int handle, int channel, int data, IntPtr user)
        {
            lyrics = "";
        }

        private void TempoSync(int handle, int channel, int data, IntPtr user)
        {
            SetTempo(true);
        }

        private void LoopSync(int handle, int channel, int data, IntPtr user)
        {
            BASS_MIDI_MARK mark = new BASS_MIDI_MARK();
            if (FindMarker(channel, "loopstart", mark))
                Bass.BASS_ChannelSetPosition(channel, mark.pos, BASSMode.BASS_POS_BYTES | BASSMode.BASS_MIDI_DECAYSEEK);
            else
                Bass.BASS_ChannelSetPosition(channel, 0, BASSMode.BASS_POS_BYTES | BASSMode.BASS_MIDI_DECAYSEEK);
        }

        private int MyWasapiProc(IntPtr buffer, Int32 length, IntPtr user)
        {
            return Bass.BASS_ChannelGetData(chan, buffer, length);
        }

        private bool FindMarker(int handle, string text, BASS_MIDI_MARK mark)
        {
            int a;
            for (a = 0; BassMidi.BASS_MIDI_StreamGetMark(handle, BASSMIDIMarker.BASS_MIDI_MARK_MARKER, a, mark); a++)
            {
                if (mark.text.ToLowerInvariant() == text.ToLowerInvariant()) return true;
            }
            return false;
        }

        private void SetTempo(bool reset)
        {
            new Thread(() =>
            {
                if (reset) miditempo = BassMidi.BASS_MIDI_StreamGetEvent(chan, 0, BASSMIDIEvent.MIDI_EVENT_TEMPO);                  // get the file's tempo
                BassMidi.BASS_MIDI_StreamEvent(chan, 0, BASSMIDIEvent.MIDI_EVENT_TEMPO, Convert.ToInt32(miditempo * temposcale));   // set tempo
            }).Start();
        }

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (IsWinXPOrOlder())
            {
                if (!Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
                {
                    Error("Can't initialize device");
                    return;
                }
            }
            else
            {
                Bass.BASS_Init(0, 44100, BASSInit.BASS_DEVICE_NOSPEAKER, IntPtr.Zero);
                BassWasapi.BASS_WASAPI_Init(-1, 0, 2, BASSWASAPIInit.BASS_WASAPI_BUFFER | BASSWASAPIInit.BASS_WASAPI_SHARED, 0, 0, null, IntPtr.Zero);
                BASS_WASAPI_DEVICEINFO info = new BASS_WASAPI_DEVICEINFO();
                BassWasapi.BASS_WASAPI_GetDeviceInfo(BassWasapi.BASS_WASAPI_GetDevice(), info);
                BassWasapi.BASS_WASAPI_Free();
                Bass.BASS_Free();
                if (!Bass.BASS_Init(0, info.mixfreq, BASSInit.BASS_DEVICE_NOSPEAKER, IntPtr.Zero))
                {
                    Error("Can't initialize device");
                    return;
                }
            }

            GetInfoFromStream.RunWorkerAsync();
            KSIntegration.RunWorkerAsync();

            VoiceBar.Value = 100;

            VoiceBar.ContextMenu = VoiceUnlock;
            Position.ContextMenu = ShowTime;

            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_MIDI_VOICES, 100000);
        }

        private void ResetEverything_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift)
            {
                MessageBox.Show("My soundcard smelled of peanuts!!!", "Help-a me!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Confirm user wants to close
                switch (MessageBox.Show(this, "Are you sure?", "Reset", MessageBoxButtons.YesNo))
                {
                    case DialogResult.No:
                        break;
                    default:
                        Bass.BASS_StreamFree(chan);
                        BassWasapi.BASS_WASAPI_Free();
                        break;
                }
            }
        }

        private void OpenMIDIButton_Click(object sender, EventArgs e)
        {
            if (OpenMIDI.ShowDialog() == DialogResult.OK)
            {
                Bass.BASS_StreamFree(chan);
                if (!IsWinXPOrOlder())
                {
                    WasapiProc = new WASAPIPROC(MyWasapiProc);
                    BassWasapi.BASS_WASAPI_Free();
                    BassWasapi.BASS_WASAPI_Init(-1, 0, 2, BASSWASAPIInit.BASS_WASAPI_BUFFER | BASSWASAPIInit.BASS_WASAPI_SHARED, 0.05f, 0, WasapiProc, IntPtr.Zero);
                }
                LyricsFromStream.Text = "";
                if ((chan = BassMidi.BASS_MIDI_StreamCreateFile(OpenMIDI.FileName, 0L, 0L,
                    (IsWinXPOrOlder() ? BASSFlag.BASS_DEFAULT : (BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_SAMPLE_FLOAT)) | BASSFlag.BASS_MIDI_DECAYEND | (RvAndChr.Checked ? BASSFlag.BASS_MIDI_NOFX : 0),
                    0)) == 0)
                {
                    OpenMIDIButton.Text = "Click here to open a file...";
                    MIDITitle.Text = "";
                    Position.Text = "-";
                    Error("Can't play the file");
                    return;
                }
                Bass.BASS_ChannelSetAttribute(chan, BASSAttribute.BASS_ATTRIB_MIDI_VOICES, VoiceBar.Value); // apply to current MIDI file too
                Bass.BASS_ChannelSetAttribute(chan, BASSAttribute.BASS_ATTRIB_MIDI_CPU, CPUBar.Value); // apply to current MIDI file too
                OpenMIDIButton.Text = OpenMIDI.FileName;
                { // set the title (track name of first track)
                    BASS_MIDI_MARK mark = new BASS_MIDI_MARK();
                    if (BassMidi.BASS_MIDI_StreamGetMark(chan, BASSMIDIMarker.BASS_MIDI_MARK_TRACK, 0, mark) && mark.track == 0)
                        MIDITitle.Text = mark.text;
                    else
                        MIDITitle.Text = "";
                }
                TrackbarStream.Minimum = 0;
                TrackbarStream.Maximum = (int)Bass.BASS_ChannelGetLength(chan, BASSMode.BASS_POS_MIDI_TICK) / 120;
                { // set looping syncs
                    BASS_MIDI_MARK mark = new BASS_MIDI_MARK();
                    LoopSyncProc = new SYNCPROC(LoopSync);
                    if (FindMarker(chan, "loopend", mark)) // found a loop end point
                        Bass.BASS_ChannelSetSync(chan, BASSSync.BASS_SYNC_POS | BASSSync.BASS_SYNC_MIXTIME, mark.pos, LoopSyncProc, IntPtr.Zero); // set a sync there

                    Bass.BASS_ChannelSetSync(chan, BASSSync.BASS_SYNC_END | BASSSync.BASS_SYNC_MIXTIME, 0, LoopSyncProc, IntPtr.Zero); // set one at the end too (eg. in case of seeking past the loop point)
                }
                { // clear lyrics buffer and set lyrics syncs
                    BASS_MIDI_MARK mark = new BASS_MIDI_MARK();
                    LyricSyncProc = new SYNCPROC(LyricSync);
                    EndSyncProc = new SYNCPROC(EndSync);
                    lyrics = "";
                    if (BassMidi.BASS_MIDI_StreamGetMark(chan, BASSMIDIMarker.BASS_MIDI_MARK_LYRIC, 0, mark)) // got lyrics
                        Bass.BASS_ChannelSetSync(chan, BASSSync.BASS_SYNC_MIDI_MARKER, (long)BASSMIDIMarker.BASS_MIDI_MARK_LYRIC, LyricSyncProc, (IntPtr)BASSMIDIMarker.BASS_MIDI_MARK_LYRIC);
                    else if (BassMidi.BASS_MIDI_StreamGetMark(chan, BASSMIDIMarker.BASS_MIDI_MARK_TEXT, 20, mark)) // got text instead (over 20 of them)
                        Bass.BASS_ChannelSetSync(chan, BASSSync.BASS_SYNC_MIDI_MARKER, (long)BASSMIDIMarker.BASS_MIDI_MARK_TEXT, LyricSyncProc, (IntPtr)BASSMIDIMarker.BASS_MIDI_MARK_TEXT);
                    Bass.BASS_ChannelSetSync(chan, BASSSync.BASS_SYNC_END, 0, EndSyncProc, IntPtr.Zero);
                }
                { // override the initial tempo, and set a sync to override tempo events and another to override after seeking
                    SetTempo(true);
                    TempoSyncProc = new SYNCPROC(TempoSync);
                    Bass.BASS_ChannelSetSync(chan, BASSSync.BASS_SYNC_MIDI_EVENT | BASSSync.BASS_SYNC_MIXTIME, (long)BASSMIDIEvent.MIDI_EVENT_TEMPO, TempoSyncProc, IntPtr.Zero);
                    Bass.BASS_ChannelSetSync(chan, BASSSync.BASS_SYNC_SETPOS | BASSSync.BASS_SYNC_MIXTIME, 0, TempoSyncProc, IntPtr.Zero);
                }
                { // get default soundfont in case of matching soundfont being used
                    BASS_MIDI_FONT[] sf = { new BASS_MIDI_FONT(font, -1, 0) };
                    // now set them
                    BassMidi.BASS_MIDI_StreamSetFonts(chan, sf, sf.Length);
                }
            }
            Bass.BASS_ChannelSetAttribute(chan, BASSAttribute.BASS_ATTRIB_MIDI_CPU, 95);
            if (PlayPauseBtn.Text != "Pause")
            {
                if (IsWinXPOrOlder()) Bass.BASS_ChannelPlay(chan, true);
                else BassWasapi.BASS_WASAPI_Start();
            }
        }

        private void LoadSoundfont_Click(object sender, EventArgs e)
        {
            if (OpenSF.ShowDialog() == DialogResult.OK)
            {
                int newfont = BassMidi.BASS_MIDI_FontInit(OpenSF.FileName, 0);
                if (newfont != 0)
                {
                    BASS_MIDI_FONT[] sf = { new BASS_MIDI_FONT(newfont, -1, 0) };
                    // now set them
                    BassMidi.BASS_MIDI_StreamSetFonts(chan, sf, sf.Length);
                    font = newfont;
                }
                else Error("Can't load soundfont");
            }
        }

        string sfinfolabel;
        int maxcpu = 95;
        int maxvoices = 100;
        float active = 0.0f;
        float cpu = 0.0f;
        long tick;
        long lentick;
        long normalpos;
        long normallen;
        TimeSpan PassedTime;
        TimeSpan LengthTime;
        private void GetInfo_Tick(object sender, EventArgs e)
        {
            if (!ShowTimeNotTick.Checked)
            {
                Position.Text = tick.ToString();
                Position.Text = String.Format("{0}/{1}", tick.ToString(), lentick.ToString());
            }
            else
            {
                Position.Text = String.Format("{0}/{1}", 
                    String.Format("{0:0}:{1:00}", PassedTime.Minutes, PassedTime.Seconds),
                    String.Format("{0:0}:{1:00}", LengthTime.Minutes, LengthTime.Seconds)
                    );
            }

            LyricsFromStream.Text = lyrics;
            TrackbarStream.Value = Convert.ToInt32(tick / 120);
            TempoCurrent.Text = String.Format("{0}", Convert.ToDouble(60000000 / (miditempo * temposcale)).ToString("0.0"));
            VoicesLabel.Text = String.Format("Voices: {0}/{1}", Convert.ToInt32(active), VoiceBar.Value);
            CPUUsage.Text = String.Format("CPU: {0}%/{1}%", Convert.ToInt32(cpu), CPUBar.Value);
            SFInfoLabel.Text = sfinfolabel;
        }

        private void GetInfoFromStream_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (chan != 0)
                {
                    normalpos = Bass.BASS_ChannelGetPosition(chan);
                    normallen = Bass.BASS_ChannelGetLength(chan);
                    tick = Bass.BASS_ChannelGetPosition(chan, BASSMode.BASS_POS_MIDI_TICK); // get position in ticks
                    lentick = Bass.BASS_ChannelGetLength(chan, BASSMode.BASS_POS_MIDI_TICK); // get length in ticks
                    Bass.BASS_ChannelSetAttribute(chan, BASSAttribute.BASS_ATTRIB_MIDI_VOICES, maxvoices); // apply to current MIDI file too
                    Bass.BASS_ChannelSetAttribute(chan, BASSAttribute.BASS_ATTRIB_MIDI_CPU, maxcpu); // apply to current MIDI file too
                    Bass.BASS_ChannelGetAttribute(chan, BASSAttribute.BASS_ATTRIB_MIDI_VOICES_ACTIVE, ref active); // get active voices
                    Bass.BASS_ChannelGetAttribute(chan, BASSAttribute.BASS_ATTRIB_CPU, ref cpu); // get cpu usage
                    PassedTime = TimeSpan.FromSeconds(Bass.BASS_ChannelBytes2Seconds(chan, normalpos));
                    LengthTime = TimeSpan.FromSeconds(Bass.BASS_ChannelBytes2Seconds(chan, normallen));
                }

                BASS_MIDI_FONTINFO i = new BASS_MIDI_FONTINFO();
                BassMidi.BASS_MIDI_FontGetInfo(font, i);
                sfinfolabel = String.Format("Name: {0}\nLoaded: {1} / {2}", i.name, i.samload, i.samsize);

                System.Threading.Thread.Sleep(1);
            }
        }

        private void KSIntegration_DoWork(object sender, DoWorkEventArgs e)
        {
            using (RegistryKey Mixer = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Keppy's Synthesizer\", true))
            {
                if (Mixer != null && !IsWinXPOrOlder())
                {
                    RegistryKey SynthSettings = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Keppy's Synthesizer\\Settings", true);
                    RegistryKey Channels = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Keppy's Synthesizer\\Channels", true);
                    RegistryKey Watchdog = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Keppy's Synthesizer\\Watchdog", true);
                    RegistryKey SynthPaths = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Keppy's Synthesizer\\Paths", true);
                    while (isitrunning)
                    {
                        try
                        {
                            if (chan != 0)
                            {
                                int levels = BassWasapi.BASS_WASAPI_GetLevel();

                                Mixer.SetValue("leftvol", Utils.LowWord32(levels), RegistryValueKind.DWord);
                                Mixer.SetValue("rightvol", Utils.HighWord32(levels), RegistryValueKind.DWord);
                                Mixer.SetValue("currentcpuusage0", cpu, RegistryValueKind.DWord);
                                for (int i = 1; i <= 16; i++)
                                {
                                    Mixer.SetValue(String.Format("chv{0}", i), BassMidi.BASS_MIDI_StreamGetEvent(chan, i - 1, (BASSMIDIEvent)0x20001), RegistryValueKind.DWord);
                                    BassMidi.BASS_MIDI_StreamEvent(chan, i - 1, BASSMIDIEvent.MIDI_EVENT_MIXLEVEL, Convert.ToInt32(Channels.GetValue(String.Format("ch{0}", i))));
                                }
                            }

                            Watchdog.SetValue("currentapp", "BASSMIDI Test CSharp by Kep", RegistryValueKind.String);
                            if (IntPtr.Size == 8) Watchdog.SetValue("bit", "64-bit", RegistryValueKind.String);
                            else if (IntPtr.Size == 4) Watchdog.SetValue("bit", "32-bit", RegistryValueKind.String);

                            System.Threading.Thread.Sleep(1);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
        }

        private void RvAndChr_CheckedChanged(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                if (RvAndChr.Checked)
                    Bass.BASS_ChannelFlags(chan, 0, BASSFlag.BASS_MIDI_NOFX); // enable FX
                else
                    Bass.BASS_ChannelFlags(chan, BASSFlag.BASS_MIDI_NOFX, BASSFlag.BASS_MIDI_NOFX); // disable FX
            }).Start();
        }

        private void TrackbarStream_Scroll(object sender, EventArgs e)
        {
            Bass.BASS_ChannelSetPosition(chan, TrackbarStream.Value * 120, BASSMode.BASS_POS_MIDI_TICK);
            lyrics = "";
            LyricsFromStream.Text = "";
        }

        private void VoiceBar_Scroll(object sender, EventArgs e)
        {
            maxvoices = VoiceBar.Value;
        }

        private void CPUBar_Scroll(object sender, EventArgs e)
        {
            maxcpu = CPUBar.Value;
        }

        private void Tempo_Scroll(object sender, EventArgs e)
        {
            temposcale = 1 / ((30 - Tempo.Value) / 20.0f); // up to +/- 50% bpm
            SetTempo(false); // apply the tempo adjustment
        }

        private void DoUnlock_Click(object sender, EventArgs e)
        {
            if (!DoUnlock.Checked)
            {
                DoUnlock.Checked = true;
                VoiceBar.Maximum = 100000;
            }
            else
            {
                DoUnlock.Checked = false;
                VoiceBar.Maximum = 2000;
            }
        }

        private void ShowTimeNotTick_Click(object sender, EventArgs e)
        {
            if (!ShowTimeNotTick.Checked) ShowTimeNotTick.Checked = true;
            else ShowTimeNotTick.Checked = false;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                base.OnFormClosing(e);

                if (e.CloseReason == CloseReason.WindowsShutDown) return;

                // Confirm user wants to close
                switch (MessageBox.Show(this, "Are you sure you want to close?", "Closing", MessageBoxButtons.YesNo))
                {
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                    default:
                        if (!IsWinXPOrOlder()) BassWasapi.BASS_WASAPI_Free();
                        Bass.BASS_Free();

                        isitrunning = false;
                        Thread.Sleep(100);

                        using (RegistryKey Mixer = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Keppy's Synthesizer\", true))
                        {
                            if (Mixer != null)
                            {
                                RegistryKey SynthSettings = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Keppy's Synthesizer\\Settings", true);
                                RegistryKey Channels = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Keppy's Synthesizer\\Channels", true);
                                RegistryKey Watchdog = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Keppy's Synthesizer\\Watchdog", true);
                                RegistryKey SynthPaths = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Keppy's Synthesizer\\Paths", true);
                                try
                                {
                                    Mixer.SetValue("leftvol", "0", RegistryValueKind.DWord);
                                    Mixer.SetValue("rightvol", "0", RegistryValueKind.DWord);
                                    Mixer.SetValue("currentcpuusage0", "0", RegistryValueKind.DWord);
                                    Mixer.SetValue("currentvoices0", "0", RegistryValueKind.DWord);
                                    for (int i = 1; i <= 16; i++) Mixer.SetValue(String.Format("chv{0}", i), "0", RegistryValueKind.DWord);

                                    Watchdog.SetValue("currentapp", "None", RegistryValueKind.String);
                                    Watchdog.SetValue("bit", "...", RegistryValueKind.String);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString());
                                }
                            }
                        }

                        Application.ExitThread();
                        break;
                }
            }
            catch { }
        }

        private void PlayPauseBtn_Click(object sender, EventArgs e)
        {
            if (PlayPauseBtn.Text == "Play")
            {
                if (chan != 0)
                {
                    PlayPauseBtn.Text = "Pause";
                    if (IsWinXPOrOlder()) Bass.BASS_ChannelPause(chan);
                    else BassWasapi.BASS_WASAPI_Stop(false);
                }
                else PlayPauseBtn.Text = "Pause";
            }
            else
            {
                if (chan != 0)
                {
                    PlayPauseBtn.Text = "Play";
                    if (IsWinXPOrOlder()) Bass.BASS_ChannelPlay(chan, false);
                    else BassWasapi.BASS_WASAPI_Start();
                }
                else PlayPauseBtn.Text = "Play";
            }
        }

        public int MakeLong(short lowPart, short highPart)
        {
            return (int)(((ushort)lowPart) | (uint)(highPart << 16));
        }
    }
}
