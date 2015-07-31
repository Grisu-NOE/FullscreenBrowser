// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextBoxWithEllipsis.cs" company="Freiwillige Feuerwehr Krems/Donau">
//     Freiwillige Feuerwehr Krems/Donau
//     Austraße 33
//     A-3500 Krems/Donau
//     Austria
// 
//     Tel.:   +43 (0)2732 85522
//     Fax.:   +43 (0)2732 85522 40
//     E-mail: office@feuerwehr-krems.at
// 
//     This software is furnished under a license and may be
//     used  and copied only in accordance with the terms of
//     such  license  and  with  the  inclusion of the above
//     copyright  notice.  This software or any other copies
//     thereof   may  not  be  provided  or  otherwise  made
//     available  to  any  other  person.  No  title  to and
//     ownership of the software is hereby transferred.
// 
//     The information in this software is subject to change
//     without  notice  and  should  not  be  construed as a
//     commitment by Freiwillige Feuerwehr Krems/Donau.
// 
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace At.FF.Krems.Utils.WPF.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>Enumeration for specifying where the ellipsis should appear.</summary>
    public enum EllipsisPlacement
    {
        /// <summary>The left.</summary>
        Left,

        /// <summary>The center.</summary>
        Center,

        /// <summary>The right.</summary>
        Right
    }

    /// <summary>
    /// This is a subclass of TextBox with the ability to show an ellipsis 
    /// when the Text doesn't fit in the visible area.
    /// </summary>
    public class TextBoxWithEllipsis : TextBox
    {
        #region Fields

        /// <summary>
        /// Last length of substring of LongText known to fit.
        /// Used while calculating the correct length to fit.
        /// </summary>
        private int lastFitLen;

        /// <summary>
        /// Last length of substring of LongText known to be too long.
        /// Used while calculating the correct length to fit.
        /// </summary>
        private int lastLongLen;

        /// <summary>
        /// Length of substring of LongText currently assigned to the Text property.
        /// Used while calculating the correct length to fit.
        /// </summary>
        private int curLen;

        /// <summary>Used to detect whether the OnTextChanged event occurs due to an external change VS. an internal one.</summary>
        private bool externalChange = true;

        /// <summary>Used to disable ellipsis internally (primarily while the control has the focus).</summary>
        private bool internalEnabled = true;

        /// <summary>Backer for LongText.</summary>
        private string longText = string.Empty;

        /// <summary>Backer for UseLongTextForToolTip.</summary>
        private bool externalEnabled = true;

        /// <summary>Backer for UseLongTextForToolTip.</summary>
        private bool useLongTextForToolTip;

        /// <summary>Backer for EllipsisPlacement.</summary>
        private EllipsisPlacement placement;

        #endregion

        #region Ctor

        /// <summary>Initializes a new instance of the <see cref="TextBoxWithEllipsis"/> class.</summary>
        public TextBoxWithEllipsis()
        {
            // Initialize inherited stuff as desired.
            this.IsReadOnlyCaretVisible = true;

            // Initialize stuff added by this class
            this.IsEllipsisEnabled = true;
            this.UseLongTextForToolTip = true;
            this.FudgePix = 3.0;
            this.placement = EllipsisPlacement.Right;
            this.internalEnabled = true;

            this.LayoutUpdated += this.TextBoxWithEllipsisLayoutUpdated;
            this.SizeChanged += this.TextBoxWithEllipsisSizeChanged;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the underlying text that gets truncated with ellipsis if it doesn't fit.
        /// Setting this and setting Text has the same effect, but getting Text may
        /// get a truncated version of LongText.
        /// </summary>
        /// <value>The long text.</value>
        public string LongText
        {
            get
            {
                return this.longText;
            }

            set
            {
                this.longText = value ?? string.Empty;
                this.PrepareForLayout();
            }
        }

        /// <summary>Gets or sets the ellipsis placement.</summary>
        /// <value>The ellipsis placement.</value>
        public EllipsisPlacement EllipsisPlacement
        {
            get
            {
                return this.placement;
            }

            set
            {
                if (this.placement == value)
                {
                    return;
                }

                this.placement = value;

                if (this.DoEllipsis)
                {
                    this.PrepareForLayout();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether if true, Text/LongText will be truncated with ellipsis
        /// to fit in the visible area of the TextBox (except when it has the focus).
        /// </summary>
        /// <value><c>true</c> if [is ellipsis enabled]; otherwise, <c>false</c>.</value>
        public bool IsEllipsisEnabled
        {
            get
            {
                return this.externalEnabled;
            }

            set
            {
                this.externalEnabled = value;
                this.PrepareForLayout();

                if (this.DoEllipsis)
                {
                    // Since we didn't change Text or Size, layout wasn't performed 
                    // as a side effect.  Pretend that it was.
                    this.TextBoxWithEllipsisLayoutUpdated(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether if true, ToolTip will be set to LongText whenever
        /// LongText doesn't fit in the visible area.  
        /// If false, ToolTip will be set to null unless
        /// the user sets it to something other than LongText.
        /// </summary>
        public bool UseLongTextForToolTip
        {
            get
            {
                return this.useLongTextForToolTip;
            }

            set
            {
                if (this.useLongTextForToolTip == value)
                {
                    return;
                }

                this.useLongTextForToolTip = value;

                if (value)
                {
                    // When turning it on, set ToolTip to
                    // longText if the current Text is too long.
                    if (this.ExtentWidth > this.ViewportWidth ||
                        this.Text != this.longText)
                    {
                        this.ToolTip = this.longText;
                    }
                }
                else
                {
                    // When turning it off, set ToolTip to null
                    // unless user has set it to something other
                    // than longText;
                    if (this.longText.Equals(this.ToolTip))
                    {
                        this.ToolTip = null;
                    }
                }
            }
        }

        /// <summary>Gets or sets the fudge pix.</summary>
        /// <value>The fudge pix.</value>
        public double FudgePix { get; set; }

        /// <summary>Gets a value indicating whether do ellipsis.</summary>
        /// <value><c>true</c> if [do ellipsis]; otherwise, <c>false</c>.</value>
        private bool DoEllipsis
        {
            get
            {
                return this.IsEllipsisEnabled && this.internalEnabled;
            }
        }

        #endregion

        /// <summary>
        /// OnTextChanged is overridden so we can avoid 
        /// raising the TextChanged event when we change 
        /// the Text property internally while searching 
        /// for the longest substring that fits.
        /// If Text is changed externally, we copy the
        /// new Text into LongText before we overwrite Text 
        /// with the truncated version (if IsEllipsisEnabled).
        /// </summary>
        /// <param name="e">The arguments that are associated with the <see cref="E:System.Windows.Controls.Primitives.TextBoxBase.TextChanged" /> event.</param>
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (this.externalChange)
            {
                this.longText = this.Text ?? string.Empty;
                if (this.UseLongTextForToolTip)
                {
                    this.ToolTip = this.longText;
                }

                this.PrepareForLayout();
                base.OnTextChanged(e);
            }
        }

        /// <summary>
        /// Invoked whenever an unhandled <see cref="E:System.Windows.Input.Keyboard.GotKeyboardFocus" /> attached routed event reaches an element derived from this class in its route. Implement this method to add class handling for this event.
        /// Makes the entire text available for editing, selecting, and scrolling until focus is lost.
        /// </summary>
        /// <param name="e">Provides data about the event.</param>
        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            this.internalEnabled = false;
            this.SetText(this.longText);
            base.OnGotKeyboardFocus(e);
        }

        /// <summary>
        /// Invoked whenever an unhandled <see cref="E:System.Windows.Input.Keyboard.LostKeyboardFocus" /> attached routed event reaches an element derived from this class in its route. Implement this method to add class handling for this event.
        /// Returns to trimming and showing ellipsis.
        /// </summary>
        /// <param name="e">Provides data about the event.</param>
        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            this.internalEnabled = true;
            this.PrepareForLayout();
            base.OnLostKeyboardFocus(e);
        }

        /// <summary>Sets the Text property without raising the TextChanged event.</summary>
        /// <param name="text">The text.</param>
        private void SetText(string text)
        {
            if (this.Text == text)
            {
                return;
            }

            this.externalChange = false;
            this.Text = text; // Will trigger Layout event.
            this.externalChange = true;
        }

        /// <summary>
        /// Arranges for the next LayoutUpdated event to trim longText and add ellipsis.
        /// Also triggers layout by setting Text.
        /// </summary>
        private void PrepareForLayout()
        {
            this.lastFitLen = 0;
            this.lastLongLen = this.longText.Length;
            this.curLen = this.longText.Length;

            // This raises the LayoutUpdated event, whose
            // handler does the ellipsis.
            this.SetText(this.longText);
        }

        /// <summary>Texts the box with ellipsis size changed.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SizeChangedEventArgs"/> instance containing the event data.</param>
        private void TextBoxWithEllipsisSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.DoEllipsis && Math.Abs(e.NewSize.Width - e.PreviousSize.Width) > 0)
            {
                // We need to recalculate the longest substring of LongText that will fit (with ellipsis).
                // Prepare for the LayoutUpdated event, which does the recalc and is raised after this.
                this.PrepareForLayout();
            }
        }

        /// <summary>
        /// Texts the box with ellipsis layout updated.
        /// Called when Text or Size changes (and maybe at other times we don't care about).
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TextBoxWithEllipsisLayoutUpdated(object sender, EventArgs e)
        {
            if (this.DoEllipsis)
            {
                // This does a binary search (bisection) to determine the maximum substring
                // of _longText that will fit in visible area.  Instead of a loop, it
                // uses a type of recursion that happens because this event is raised
                // again if we set the Text property in here.
                if (this.ViewportWidth + this.FudgePix < this.ExtentWidth)
                {
                    // The current Text (whose length without ellipsis is _curLen) is too long.
                    this.lastLongLen = this.curLen;
                }
                else
                {
                    // The current Text is not too long.
                    this.lastFitLen = this.curLen;
                }

                // Try a new substring whose length is halfway between the last length
                // known to fit and the last length known to be too long.
                var newLen = (this.lastFitLen + this.lastLongLen) / 2;

                if (this.curLen == newLen)
                {
                    // We're done! Usually, _lastLongLen is _lastFitLen + 1.
                    if (!this.UseLongTextForToolTip)
                    {
                        return;
                    }

                    this.ToolTip = this.Text == this.longText ? null : this.longText;
                }
                else
                {
                    this.curLen = newLen;

                    // This sets the Text property without raising the TextChanged event.
                    // However it does raise the LayoutUpdated event again, though
                    // not recursively.
                    this.CalcText();
                }
            }
            else if (this.UseLongTextForToolTip)
            {
                this.ToolTip = this.ViewportWidth < this.ExtentWidth ? this.longText : null;
            }
        }

        /// <summary>Sets Text to a substring of longText based on placement and curLen..</summary>
        /// <exception cref="System.Exception">Unexpected switch value:  + this.placement.ToString()</exception>
        private void CalcText()
        {
            switch (this.placement)
            {
                case EllipsisPlacement.Right:
                    this.SetText(this.longText.Substring(0, this.curLen) + "\u2026");
                    break;

                case EllipsisPlacement.Center:
                    var firstLen = this.curLen / 2;
                    var secondLen = this.curLen - firstLen;
                    this.SetText(this.longText.Substring(0, firstLen) + "\u2026" + this.longText.Substring(this.longText.Length - secondLen));
                    break;

                case EllipsisPlacement.Left:
                    var start = this.longText.Length - this.curLen;
                    this.SetText("\u2026" + this.longText.Substring(start));
                    break;

                default:
                    throw new Exception("Unexpected switch value: " + this.placement.ToString());
            }
        }
    }
}