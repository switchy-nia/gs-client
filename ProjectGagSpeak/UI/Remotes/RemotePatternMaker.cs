using CkCommons;
using CkCommons.Gui;
using Dalamud.Interface.Utility;
using Dalamud.Interface.Utility.Raii;
using GagSpeak.Services.Mediator;
using GagSpeak.Services.Textures;
using GagSpeak.Services.Tutorial;
using GagSpeak.State.Handlers;
using GagSpeak.State.Managers;
using ImGuiNET;
using OtterGui;
using System.Timers;

namespace GagSpeak.Gui.UiRemote;

//public class RemotePatternMaker : RemoteBase
//{
//    private readonly string _windowName;
//    public RemotePatternMaker(ILogger<RemotePatternMaker> logger, GagspeakMediator mediator,
//        BuzzToyManager manager, RemoteHandler handler, TutorialService guides, string winName = "Pattern Creator") 
//        : base(logger, mediator, handler, manager, guides, winName)
//    {
//        _windowName = winName;
//    }

//    // The storage buffer of all recorded vibration data in byte format. Eventually stored into a pattern.
//    public List<double> StoredVibrationData = new List<double>();
//    public bool IsRecording { get; protected set; } = false;
//    public bool FinishedRecording { get; protected set; } = false;

//    /// <summary> Will display personal devices, their motors and additional options. </summary>
//    public override void DrawCenterBar(ref float xPos, ref float yPos, ref float width)
//    {
//        // grab the content region of the current section
//        var CurrentRegion = ImGui.GetContentRegionAvail();
//        ImGui.SetCursorPosY(ImGui.GetCursorPosY() - ImGuiHelpers.GlobalScale * 5);
//        using (var color = ImRaii.PushColor(ImGuiCol.ChildBg, new Vector4(0.2f, 0.2f, 0.2f, 0.930f)))
//        {
//            // create a child for the center bar
//            using (var canterBar = ImRaii.Child($"###CenterBarDrawPersonal", new Vector2(CurrentRegion.X, 40f), false))
//            {
//                // Dummy bar.
//            }
//        }
//    }


//    /// <summary>
//    /// This method is also an overrided function, as depending on the use.
//    /// We may also implement unique buttons here on the side that execute different functionalities.
//    /// </summary>
//    /// <param name="region"> The region of the side button section of the UI </param>
//    public override void DrawSideButtonsTable(Vector2 region)
//    {
//        // push our styles
//        using var styleColor = ImRaii.PushColor(ImGuiCol.Button, new Vector4(.2f, .2f, .2f, .2f))
//            .Push(ImGuiCol.ButtonHovered, new Vector4(.3f, .3f, .3f, .4f))
//            .Push(ImGuiCol.ButtonActive, CkColor.LushPinkButton.Uint());
//        using var styleVar = ImRaii.PushStyle(ImGuiStyleVar.FrameRounding, 40);

//        // grab the content region of the current section
//        var CurrentRegion = ImGui.GetContentRegionAvail();
//        var yPos2 = ImGui.GetCursorPosY();

//        // setup a child for the table cell space
//        using (var leftChild = ImRaii.Child($"###ButtonsList", CurrentRegion with { Y = region.Y }, false, WFlags.NoDecoration))
//        {
//            var InitPos = ImGui.GetCursorPosY();
//            if (IsRecording)
//            {
//                ImGui.AlignTextToFramePadding();
//                ImGuiUtil.Center($"{DurationStopwatch.Elapsed.ToString(@"mm\:ss")}");
//            }
//            else
//            {
//                ImGui.AlignTextToFramePadding();
//                ImGuiUtil.Center("00:00");
//            }
//            // _guides.OpenTutorial(TutorialType.Patterns, StepsPatterns.RecordedDuration, CurrentPos, CurrentSize);

//            // move our yposition down to the top of the frame height times a .3f scale of the current region
//            ImGui.SetCursorPosY(InitPos + CurrentRegion.Y * .1f);
//            ImGui.Separator();
//            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + 7f);

//            // attempt to obtain an image wrap for it
//            if (CosmeticService.CoreTextures.Cache[CoreTexture.ArrowSpin] is { } wrap)
//            {
//                var buttonColor = IsLooping ? CkColor.LushPinkButton.Vec4() : CkColor.SideButton.Vec4();
//                // aligns the image in the center like we want.
//                if (CkGui.DrawScaledCenterButtonImage("LoopButton"+ _windowName, new Vector2(50, 50),
//                    buttonColor, new Vector2(40, 40), wrap))
//                {
//                    IsLooping = !IsLooping;
//                    if (IsFloating) { IsFloating = false; }
//                }
//            }
//            // _guides.OpenTutorial(TutorialType.Patterns, StepsPatterns.LoopButton, CurrentPos, CurrentSize);


//            // move it down from current position by another .2f scale
//            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + CurrentRegion.Y * .05f);

//            if (CosmeticService.CoreTextures.Cache[CoreTexture.CircleDot] is { } wrap2)
//            {
//                var buttonColor2 = IsFloating ? CkColor.LushPinkButton.Vec4() : CkColor.SideButton.Vec4();
//                // aligns the image in the center like we want.
//                if (CkGui.DrawScaledCenterButtonImage("FloatButton" + _windowName, new Vector2(50, 50),
//                    buttonColor2, new Vector2(40, 40), wrap2))
//                {
//                    IsFloating = !IsFloating;
//                    if (IsLooping) { IsLooping = false; }
//                }
//            }
//            // _guides.OpenTutorial(TutorialType.Patterns, StepsPatterns.FloatButton, CurrentPos, CurrentSize);


//            ImGui.SetCursorPosY(CurrentRegion.Y * .775f);
//            var buttonColor3 = IsRecording ? CkColor.LushPinkButton.Vec4() : CkColor.SideButton.Vec4();
//            // display the stop or play icon depending on if we are recording or not.
//            if (!IsRecording)
//            {
//                if (CosmeticService.CoreTextures.Cache[CoreTexture.Play] is { } wrap3)
//                {
//                    if (CkGui.DrawScaledCenterButtonImage("RecordStartButton" + _windowName, new Vector2(50, 50),
//                        buttonColor3, new Vector2(40, 40), wrap3))
//                    {
//                        _logger.LogTrace("Starting Recording!");
//                        StartVibrating();
//                    }
//                }
//                // _guides.OpenTutorial(TutorialType.Patterns, StepsPatterns.RecordingButton, CurrentPos, CurrentSize);
//            }
//            // we are recording so display stop
//            else
//            {
//                if (CosmeticService.CoreTextures.Cache[CoreTexture.Stop] is { } wrap4)
//                {
//                    if (CkGui.DrawScaledCenterButtonImage("RecordStopButton" + _windowName, new Vector2(50, 50),
//                        buttonColor3, new Vector2(40, 40), wrap4))
//                    {
//                        _logger.LogTrace("Stopping Recording!");
//                        StopVibrating();
//                    }
//                }
//                // _guides.OpenTutorial(TutorialType.Patterns, StepsPatterns.StoppingRecording, CurrentPos, CurrentSize);
//            }
//        }
//        // pop what we appended
//        styleColor.Pop(3);
//        styleVar.Pop();
//    }

//    public override void StartVibrating()
//    {
//        _logger.LogDebug($"Started Recording on parent class {_windowName}!");
//        // call the base start
//        base.StartVibrating();
//        // reset our pattern data and begin recording
//        StoredVibrationData.Clear();
//        IsRecording = true;
//    }

//    public override void StopVibrating()
//    {
//        // before we stop the stopwatch from the base call, we must make sure that we extract it here
//        var Duration = DurationStopwatch.Elapsed;
//        _logger.LogDebug($"Stopping Recording on parent class {_windowName}!");
//        // call the base stop
//        base.StopVibrating();
//        // stop recording and set that we have finished
//        IsRecording = false;
//        FinishedRecording = true;
//        // compile together and send off the popup handler for compiling a newly created pattern
//        Mediator.Publish(new PatternSavePromptMessage(StoredVibrationData, Duration));
//        // Mediator.Publish(new UiToggleMessage(typeof(RemotePatternMaker)));

//    }


//    /// <summary>
//    /// Override method for the recording data.
//    /// It is here that we decide how our class handles the recordData function for our personal remote.
//    /// </summary>
//    public override void RecordData(object? sender, ElapsedEventArgs e)
//    {
//        // add to recorded storage for the pattern
//        AddIntensityToByteStorage();

//        // handle playing to our personal vibrator configured devices.
//        PlayIntensityToDevices();
//    }

//    private void AddIntensityToByteStorage()
//    {
//        if (IsLooping && !IsDragging && StoredLoopDataBlock.Count > 0)
//        {
//            //_logger.LogTrace($"Looping & not Dragging: {(byte)Math.Round(StoredLoopDataBlock[BufferLoopIndex])}");
//            // If looping, but not dragging, and have stored LoopData, add the stored data to the vibration data.
//            StoredVibrationData.Add(Math.Round(StoredLoopDataBlock[BufferLoopIndex]));
//        }
//        else
//        {
//            //_logger.LogTrace($"Injecting new data: {(byte)Math.Round(CirclePosition[1])}");
//            // Otherwise, add the current circle position to the vibration data.
//            StoredVibrationData.Add(Math.Round(CirclePosition[1]));
//        }
//        // if we reached passed our "capped limit", (its like 3 hours) stop recording.
//        if (StoredVibrationData.Count > 270000)
//        {
//            //_logger.LogWarning("Capped the stored data, stopping recording!");
//            StopVibrating();
//        }
//    }

//    private void PlayIntensityToDevices()
//    {
//        // if any devices are currently connected, and our intiface client is connected,
//        if (true)
//        {
//            //_logger.LogTrace("Sending Vibration Data to Devices!");
//            // send the vibration data to all connected devices
//            if (IsLooping && !IsDragging && StoredLoopDataBlock.Count > 0)
//            {
//                var value = Math.Round(StoredLoopDataBlock[BufferLoopIndex], 3);
//                _logger.LogTrace($"{value}");
//                _handler.VibrateAll(value);
//            }
//            else
//            {
//                var value = Math.Round(CirclePosition[1], 3);
//                _logger.LogTrace($"{value}");
//                _handler.VibrateAll(value);
//            }
//        }
//    }
//}
