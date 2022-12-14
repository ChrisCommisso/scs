using System.Runtime.Serialization.Formatters;

using System.Diagnostics;
using System.Diagnostics.Contracts;
using System;
using System.IO;
using Microsoft.Win32;
using System.ComponentModel;
using System.Globalization;
using System.Drawing;

/// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle"]/*' />
/// <devdoc>
///    <para>
///       Stores the location and size of a rectangular region. For
///       more advanced region functions use a <see cref='System.Drawing.Region'/>
///       object.
///    </para>
/// </devdoc>

[Serializable]
[System.Runtime.InteropServices.ComVisible(true)]
public struct BoxRect
{

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Empty"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Stores the location and size of a rectangular region. For
    ///       more advanced region functions use a <see cref='System.Drawing.Region'/>
    ///       object.
    ///    </para>
    /// </devdoc>
    public static readonly BoxRect Empty = new BoxRect();

    private float x;
    private float y;
    private float width;
    private float height;

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Rectangle"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Initializes a new instance of the <see cref='System.Drawing.Rectangle'/>
    ///       class with the specified location and size.
    ///    </para>
    /// </devdoc>
    public BoxRect(float x, float y, float width, float height)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }
    public void DebugDraw(float z,UnityEngine.Color color) {
        UnityEngine.Debug.DrawLine(new UnityEngine.Vector3(x, y, z), new UnityEngine.Vector3(x, y + height, z), color);
        UnityEngine.Debug.DrawLine(new UnityEngine.Vector3(x, y + height, z), new UnityEngine.Vector3(x + width, y + height, z), color);
        UnityEngine.Debug.DrawLine(new UnityEngine.Vector3(x + width, y, z), new UnityEngine.Vector3(x + width, y + height, z), color);
        UnityEngine.Debug.DrawLine(new UnityEngine.Vector3(x, y, z), new UnityEngine.Vector3(x + width, y, z), color);
    }
    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Rectangle1"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Initializes a new instance of the Rectangle class with the specified location
    ///       and size.
    ///    </para>
    /// </devdoc>
    public BoxRect(Point location, Size size)
    {
        this.x = location.X;
        this.y = location.Y;
        this.width = size.Width;
        this.height = size.Height;
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.FromLTRB"]/*' />
    /// <devdoc>
    ///    Creates a new <see cref='System.Drawing.Rectangle'/> with
    ///    the specified location and size.
    /// </devdoc>
    // !! Not in C++ version
    public static BoxRect FromLTRB(int left, int top, int right, int bottom)
    {
        return new BoxRect(left,
                             top,
                             right - left,
                             bottom - top);
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Location"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Gets or sets the coordinates of the
    ///       upper-left corner of the rectangular region represented by this <see cref='System.Drawing.Rectangle'/>.
    ///    </para>
    /// </devdoc>

 



    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.X"]/*' />
    /// <devdoc>
    ///    Gets or sets the x-coordinate of the
    ///    upper-left corner of the rectangular region defined by this <see cref='System.Drawing.Rectangle'/>.
    /// </devdoc>
    public float X
    {
        get
        {
            return x;
        }
        set
        {
            x = value;
        }
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Y"]/*' />
    /// <devdoc>
    ///    Gets or sets the y-coordinate of the
    ///    upper-left corner of the rectangular region defined by this <see cref='System.Drawing.Rectangle'/>.
    /// </devdoc>
    public float Y
    {
        get
        {
            return y;
        }
        set
        {
            y = value;
        }
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Width"]/*' />
    /// <devdoc>
    ///    Gets or sets the width of the rectangular
    ///    region defined by this <see cref='System.Drawing.Rectangle'/>.
    /// </devdoc>
    public float Width
    {
        get
        {
            return width;
        }
        set
        {
            width = value;
        }
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Height"]/*' />
    /// <devdoc>
    ///    Gets or sets the width of the rectangular
    ///    region defined by this <see cref='System.Drawing.Rectangle'/>.
    /// </devdoc>
    public float Height
    {
        get
        {
            return height;
        }
        set
        {
            height = value;
        }
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Left"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Gets the x-coordinate of the upper-left corner of the
    ///       rectangular region defined by this <see cref='System.Drawing.Rectangle'/> .
    ///    </para>
    /// </devdoc>
    [Browsable(false)]
    public float Left
    {
        get
        {
            return X;
        }
    }
    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Top"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Gets the y-coordinate of the upper-left corner of the
    ///       rectangular region defined by this <see cref='System.Drawing.Rectangle'/>.
    ///    </para>
    /// </devdoc>
    [Browsable(false)]
    public float Top
    {
        get
        {
            return Y;
        }
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Right"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Gets the x-coordinate of the lower-right corner of the
    ///       rectangular region defined by this <see cref='System.Drawing.Rectangle'/>.
    ///    </para>
    /// </devdoc>
    [Browsable(false)]
    public float Right
    {
        get
        {
            return X + Width;
        }
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Bottom"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Gets the y-coordinate of the lower-right corner of the
    ///       rectangular region defined by this <see cref='System.Drawing.Rectangle'/>.
    ///    </para>
    /// </devdoc>
    [Browsable(false)]
    public float Bottom
    {
        get
        {
            return Y + Height;
        }
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.IsEmpty"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Tests whether this <see cref='System.Drawing.Rectangle'/> has a <see cref='System.Drawing.Rectangle.Width'/>
    ///       or a <see cref='System.Drawing.Rectangle.Height'/> of 0.
    ///    </para>
    /// </devdoc>
    [Browsable(false)]
    public bool IsEmpty
    {
        get
        {
            return height == 0 && width == 0 && x == 0 && y == 0;
            // C++ uses this definition:
            // return(Width <= 0 )|| (Height <= 0);
        }
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Equals"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Tests whether <paramref name="obj"/> is a <see cref='System.Drawing.Rectangle'/> with
    ///       the same location and size of this Rectangle.
    ///    </para>
    /// </devdoc>
    public override bool Equals(object obj)
    {
        if (!(obj is BoxRect))
            return false;

        BoxRect comp = (BoxRect)obj;

        return (comp.X == this.X) &&
        (comp.Y == this.Y) &&
        (comp.Width == this.Width) &&
        (comp.Height == this.Height);
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.operator=="]/*' />
    /// <devdoc>
    ///    <para>
    ///       Tests whether two <see cref='System.Drawing.Rectangle'/>
    ///       objects have equal location and size.
    ///    </para>
    /// </devdoc>
    public static bool operator ==(BoxRect left, BoxRect right)
    {
        return (left.X == right.X
                && left.Y == right.Y
                && left.Width == right.Width
                && left.Height == right.Height);
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.operator!="]/*' />
    /// <devdoc>
    ///    <para>
    ///       Tests whether two <see cref='System.Drawing.Rectangle'/>
    ///       objects differ in location or size.
    ///    </para>
    /// </devdoc>
    public static bool operator !=(BoxRect left, BoxRect right)
    {
        return !(left == right);
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Ceiling"]/*' />
    /// <devdoc>
    ///   Converts a RectangleF to a Rectangle by performing a ceiling operation on
    ///   all the coordinates.
    /// </devdoc>
    public static BoxRect Ceiling(RectangleF value)
    {
        return new BoxRect((int)Math.Ceiling(value.X),
                             (int)Math.Ceiling(value.Y),
                             (int)Math.Ceiling(value.Width),
                             (int)Math.Ceiling(value.Height));
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Truncate"]/*' />
    /// <devdoc>
    ///   Converts a RectangleF to a Rectangle by performing a truncate operation on
    ///   all the coordinates.
    /// </devdoc>
    public static BoxRect Truncate(RectangleF value)
    {
        return new BoxRect((int)value.X,
                             (int)value.Y,
                             (int)value.Width,
                             (int)value.Height);
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Round"]/*' />
    /// <devdoc>
    ///   Converts a RectangleF to a Rectangle by performing a round operation on
    ///   all the coordinates.
    /// </devdoc>
    public static BoxRect Round(RectangleF value)
    {
        return new BoxRect((int)Math.Round(value.X),
                             (int)Math.Round(value.Y),
                             (int)Math.Round(value.Width),
                             (int)Math.Round(value.Height));
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Contains"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Determines if the specfied point is contained within the
    ///       rectangular region defined by this <see cref='System.Drawing.Rectangle'/> .
    ///    </para>
    /// </devdoc>
    [Pure]
    public bool Contains(int x, int y)
    {
        return this.X <= x &&
        x < this.X + this.Width &&
        this.Y <= y &&
        y < this.Y + this.Height;
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Contains1"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Determines if the specfied point is contained within the
    ///       rectangular region defined by this <see cref='System.Drawing.Rectangle'/> .
    ///    </para>
    /// </devdoc>
    [Pure]
    public bool Contains(Point pt)
    {
        return Contains(pt.X, pt.Y);
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Contains2"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Determines if the rectangular region represented by
    ///    <paramref name="rect"/> is entirely contained within the rectangular region represented by 
    ///       this <see cref='System.Drawing.Rectangle'/> .
    ///    </para>
    /// </devdoc>
    [Pure]
    public bool Contains(BoxRect rect)
    {
        return (this.X <= rect.X) &&
        ((rect.X + rect.Width) <= (this.X + this.Width)) &&
        (this.Y <= rect.Y) &&
        ((rect.Y + rect.Height) <= (this.Y + this.Height));
    }

    // !! Not in C++ version
    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.GetHashCode"]/*' />
    /// <devdoc>
    ///    <para>[To be supplied.]</para>
    /// </devdoc>
    public override int GetHashCode()
    {
        return unchecked((int)((UInt32)X ^
                    (((UInt32)Y << 13) | ((UInt32)Y >> 19)) ^
                    (((UInt32)Width << 26) | ((UInt32)Width >> 6)) ^
                    (((UInt32)Height << 7) | ((UInt32)Height >> 25))));
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Inflate"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Inflates this <see cref='System.Drawing.Rectangle'/>
    ///       by the specified amount.
    ///    </para>
    /// </devdoc>
    public void Inflate(int width, int height)
    {
        this.X -= width;
        this.Y -= height;
        this.Width += 2 * width;
        this.Height += 2 * height;
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Inflate1"]/*' />
    /// <devdoc>
    ///    Inflates this <see cref='System.Drawing.Rectangle'/> by the specified amount.
    /// </devdoc>
    public void Inflate(Size size)
    {

        Inflate(size.Width, size.Height);
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Inflate2"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Creates a <see cref='System.Drawing.Rectangle'/>
    ///       that is inflated by the specified amount.
    ///    </para>
    /// </devdoc>
    // !! Not in C++
    public static BoxRect Inflate(BoxRect rect, int x, int y)
    {
        BoxRect r = rect;
        r.Inflate(x, y);
        return r;
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Intersect"]/*' />
    /// <devdoc> Creates a Rectangle that represents the intersection between this Rectangle and rect.
    /// </devdoc>
    public void Intersect(BoxRect rect)
    {
        BoxRect result = BoxRect.Intersect(rect, this);

        this.X = result.X;
        this.Y = result.Y;
        this.Width = result.Width;
        this.Height = result.Height;
    }
    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Intersect1"]/*' />
    /// <devdoc>
    ///    Creates a rectangle that represents the intersetion between a and
    ///    b. If there is no intersection, null is returned.
    /// </devdoc>
    public static BoxRect Intersect(BoxRect a, BoxRect b)
    {
        float x1 = Math.Max(a.X, b.X);
        float x2 = Math.Min(a.X + a.Width, b.X + b.Width);
        float y1 = Math.Max(a.Y, b.Y);
        float y2 = Math.Min(a.Y + a.Height, b.Y + b.Height);

        if (x2 >= x1
            && y2 >= y1)
        {

            return new BoxRect(x1, y1, x2 - x1, y2 - y1);
        }
        return BoxRect.Empty;
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.IntersectsWith"]/*' />
    /// <devdoc>
    ///     Determines if this rectangle intersets with rect.
    /// </devdoc>
    [Pure]
    public bool IntersectsWith(BoxRect rect)
    {
        return (rect.X < this.X + this.Width) &&
        (this.X < (rect.X + rect.Width)) &&
        (rect.Y < this.Y + this.Height) &&
        (this.Y < rect.Y + rect.Height);
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Union"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Creates a rectangle that represents the union between a and
    ///       b.
    ///    </para>
    /// </devdoc>
    [Pure]
    public static BoxRect Union(BoxRect a, BoxRect b)
    {
        float x1 = Math.Min(a.X, b.X);
        float x2 = Math.Max(a.X + a.Width, b.X + b.Width);
        float y1 = Math.Min(a.Y, b.Y);
        float y2 = Math.Max(a.Y + a.Height, b.Y + b.Height);

        return new BoxRect(x1, y1, x2 - x1, y2 - y1);
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Offset"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Adjusts the location of this rectangle by the specified amount.
    ///    </para>
    /// </devdoc>
    public void Offset(Point pos)
    {
        Offset(pos.X, pos.Y);
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.Offset1"]/*' />
    /// <devdoc>
    ///    Adjusts the location of this rectangle by the specified amount.
    /// </devdoc>
    public void Offset(int x, int y)
    {
        this.X += x;
        this.Y += y;
    }

    /// <include file='doc\Rectangle.uex' path='docs/doc[@for="Rectangle.ToString"]/*' />
    /// <devdoc>
    ///    <para>
    ///       Converts the attributes of this <see cref='System.Drawing.Rectangle'/> to a
    ///       human readable string.
    ///    </para>
    /// </devdoc>
    public override string ToString()
    {
        return "{X=" + X.ToString(CultureInfo.CurrentCulture) + ",Y=" + Y.ToString(CultureInfo.CurrentCulture) +
        ",Width=" + Width.ToString(CultureInfo.CurrentCulture) +
        ",Height=" + Height.ToString(CultureInfo.CurrentCulture) + "}";
    }
}
