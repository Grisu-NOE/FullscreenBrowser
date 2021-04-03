// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEventHook.cs" company="Freiwillige Feuerwehr Krems/Donau">
//      Freiwillige Feuerwehr Krems/Donau
//      Austraße 33
//      A-3500 Krems/Donau
//      Austria
//      Tel.:   +43 (0)2732 85522
//      Fax.:   +43 (0)2732 85522 40
//      E-mail: office@feuerwehr-krems.at
// 
//      This software is furnished under a license and may be
//      used  and copied only in accordance with the terms of
//      such  license  and  with  the  inclusion of the above
//      copyright  notice.  This software or any other copies
//      thereof   may  not  be  provided  or  otherwise  made
//      available  to  any  other  person.  No  title  to and
//      ownership of the software is hereby transferred.
// 
//      The information in this software is subject to change
//      without  notice  and  should  not  be  construed as a
//      commitment by Freiwillige Feuerwehr Krems/Donau.
// 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace At.FF.Krems.Interfaces
{
    using System;

    /// <summary>The win events.</summary>
    [Flags]
    public enum WinEvents : uint
    {
        /// <summary>The win event out of context.</summary>
        WinEventOutOfContext = 0x0000, // Events are ASYNC

        /// <summary>The win event skip own thread.</summary>
        WinEventSkipOwnThread = 0x0001, // Don't call back for events on installer's thread

        /// <summary>The win event skip own process.</summary>
        WinEventSkipOwnProcess = 0x0002, // Don't call back for events on installer's process

        /// <summary>The win event in context.</summary>
        WinEventInContext = 0x0004, // Events are SYNC, this causes your dll to be injected into every process

        /// <summary>The event min.</summary>
        EventMin = 0x00000001,

        /// <summary>The event max.</summary>
        EventMax = 0x7FFFFFFF,

        /// <summary>The event system sound.</summary>
        EventSystemSound = 0x0001,

        /// <summary>The event system alert.</summary>
        EventSystemAlert = 0x0002,

        /// <summary>The event system foreground.</summary>
        EventSystemForeground = 0x0003,

        /// <summary>The event system menu start.</summary>
        EventSystemMenuStart = 0x0004,

        /// <summary>The event system menu end.</summary>
        EventSystemMenuEnd = 0x0005,

        /// <summary>The event system menu popup start.</summary>
        EventSystemMenuPopupStart = 0x0006,

        /// <summary>The event system menu popup end.</summary>
        EventSystemMenuPopupEnd = 0x0007,

        /// <summary>The event system capture start.</summary>
        EventSystemCaptureStart = 0x0008,

        /// <summary>The event system capture end.</summary>
        EventSystemCaptureEnd = 0x0009,

        /// <summary>The event system move size start.</summary>
        EventSystemMoveSizeStart = 0x000A,

        /// <summary>The event system move size end.</summary>
        EventSystemMoveSizeEnd = 0x000B,

        /// <summary>The event system context help start.</summary>
        EventSystemContextHelpStart = 0x000C,

        /// <summary>The event system context help end.</summary>
        EventSystemContextHelpEnd = 0x000D,

        /// <summary>The event system drag drop start.</summary>
        EventSystemDragDropStart = 0x000E,

        /// <summary>The event system drag drop end.</summary>
        EventSystemDragDropEnd = 0x000F,

        /// <summary>The event system dialog start.</summary>
        EventSystemDialogStart = 0x0010,

        /// <summary>The event system dialog end.</summary>
        EventSystemDialogEnd = 0x0011,

        /// <summary>The event system scrolling start.</summary>
        EventSystemScrollingStart = 0x0012,

        /// <summary>The event system scrolling end.</summary>
        EventSystemScrollingEnd = 0x0013,

        /// <summary>The event system switch start.</summary>
        EventSystemSwitchStart = 0x0014,

        /// <summary>The event system switch end.</summary>
        EventSystemSwitchEnd = 0x0015,

        /// <summary>The event system minimize start.</summary>
        EventSystemMinimizeStart = 0x0016,

        /// <summary>The event system minimize end.</summary>
        EventSystemMinimizeEnd = 0x0017,

        /// <summary>The event system desktop switch.</summary>
        EventSystemDesktopSwitch = 0x0020,

        /// <summary>The event system end.</summary>
        EventSystemEnd = 0x00FF,

        /// <summary>The event OEM defined start.</summary>
        EventOemDefinedStart = 0x0101,

        /// <summary>The event OEM defined end.</summary>
        EventOemDefinedEnd = 0x01FF,

        /// <summary>The event UIA event id start.</summary>
        EventUiaEventIdStart = 0x4E00,

        /// <summary>The event UIA event id end.</summary>
        EventUiaEventIdEnd = 0x4EFF,

        /// <summary>The event UIA prop id start.</summary>
        EventUiaPropIdStart = 0x7500,

        /// <summary>The event UIA prop id end.</summary>
        EventUiaPropIdEnd = 0x75FF,

        /// <summary>The event console caret.</summary>
        EventConsoleCaret = 0x4001,

        /// <summary>The event console update region.</summary>
        EventConsoleUpdateRegion = 0x4002,

        /// <summary>The event console update simple.</summary>
        EventConsoleUpdateSimple = 0x4003,

        /// <summary>The event console update scroll.</summary>
        EventConsoleUpdateScroll = 0x4004,

        /// <summary>The event console layout.</summary>
        EventConsoleLayout = 0x4005,

        /// <summary>The event console start application.</summary>
        EventConsoleStartApplication = 0x4006,

        /// <summary>The event console end application.</summary>
        EventConsoleEndApplication = 0x4007,

        /// <summary>The event console end.</summary>
        EventConsoleEnd = 0x40FF,

        /// <summary>The event object create.</summary>
        EventObjectCreate = 0x8000, // hwnd ID idChild is created item

        /// <summary>The event object destroy.</summary>
        EventObjectDestroy = 0x8001, // hwnd ID idChild is destroyed item

        /// <summary>The event object show.</summary>
        EventObjectShow = 0x8002, // hwnd ID idChild is shown item

        /// <summary>The event object hide.</summary>
        EventObjectHide = 0x8003, // hwnd ID idChild is hidden item

        /// <summary>The event object reorder.</summary>
        EventObjectReorder = 0x8004, // hwnd ID idChild is parent of zordering children

        /// <summary>The event object focus.</summary>
        EventObjectFocus = 0x8005, // hwnd ID idChild is focused item

        /// <summary>The event object selection.</summary>
        EventObjectSelection = 0x8006, // hwnd ID idChild is selected item (if only one), or idChild is OBJID_WINDOW if complex

        /// <summary>The event object selection add.</summary>
        EventObjectSelectionAdd = 0x8007, // hwnd ID idChild is item added

        /// <summary>The event object selection remove.</summary>
        EventObjectSelectionRemove = 0x8008, // hwnd ID idChild is item removed

        /// <summary>The event object selection within.</summary>
        EventObjectSelectionWithin = 0x8009, // hwnd ID idChild is parent of changed selected items

        /// <summary>The event object state change.</summary>
        EventObjectStateChange = 0x800A, // hwnd ID idChild is item w/ state change

        /// <summary>The event object location change.</summary>
        EventObjectLocationChange = 0x800B, // hwnd ID idChild is moved/sized item

        /// <summary>The event object name change.</summary>
        EventObjectNameChange = 0x800C, // hwnd ID idChild is item w/ name change

        /// <summary>The event object description change.</summary>
        EventObjectDescriptionChange = 0x800D, // hwnd ID idChild is item w/ desc change

        /// <summary>The event object value change.</summary>
        EventObjectValueChange = 0x800E, // hwnd ID idChild is item w/ value change

        /// <summary>The event object parent change.</summary>
        EventObjectParentChange = 0x800F, // hwnd ID idChild is item w/ new parent

        /// <summary>The event object help change.</summary>
        EventObjectHelpChange = 0x8010, // hwnd ID idChild is item w/ help change

        /// <summary>The event object default action change.</summary>
        EventObjectDefactionChange = 0x8011, // hwnd ID idChild is item w/ def action change

        /// <summary>The event object accelerator change.</summary>
        EventObjectAcceleratorChange = 0x8012, // hwnd ID idChild is item w/ keybd accel change

        /// <summary>The event object invoked.</summary>
        EventObjectInvoked = 0x8013, // hwnd ID idChild is item invoked

        /// <summary>The event object text selection changed.</summary>
        EventObjectTextSelectionChanged = 0x8014, // hwnd ID idChild is item w? test selection change

        /// <summary>The event object content scrolled.</summary>
        EventObjectContentScrolled = 0x8015,

        /// <summary>The event system arrangement preview.</summary>
        EventSystemArrangementPreview = 0x8016,

        /// <summary>The event object end.</summary>
        EventObjectEnd = 0x80FF,

        /// <summary>The event AIA start.</summary>
        EventAiaStart = 0xA000,

        /// <summary>The event AIA end.</summary>
        EventAiaEnd = 0xAFFF
    }

    /// <summary>The EventHook interface.</summary>
    public interface IEventHook
    {
        /// <summary>Occurs when event fired.</summary>
        event NativeMethods.WinEventDelegate EventFired;

        /// <summary>The setup.</summary>
        /// <param name="winEvent">The win event.</param>
        void Setup(WinEvents winEvent);

        /// <summary>Setups the specified event minimum.</summary>
        /// <param name="eventMin">The event minimum.</param>
        /// <param name="eventMax">The event maximum.</param>
        void Setup(WinEvents eventMin, WinEvents eventMax);

        /// <summary>Starts this instance.</summary>
        void Start();

        /// <summary>Stops this instance.</summary>
        void Stop();
    }
}