using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.IO;
using OpenTK.Input;

namespace GPUGraphics2D
{
/// <summary>
/// The type of vertex to use in buffer aswell as their value in bytes
/// </summary>
public enum VertexFormat
{
    XY = 8,                  //2D Vertex
    XY_COLOR = 12,           //2D Vertex with color
    XYZ = 12,                //Vertex
    XYZ_COLOR = 16,          //VertexColor
    XYZ_UV = 20,             //VertexUV
    XYZ_UV_COLOR = 24,       //VertexUVColor
    XYZ_NORMAL_UV = 32,      //VertexNormalUV
    XYZ_NORMAL_UV_COLOR = 36 //VertexNormalUVColor
}

public class VertexBuffer
{
    public VertexFormat Format { get; private set; }
    public int Stride { get; private set; }
    public int TriangleCount { get { return index_data.Count / 3; } }
    public int VertexCount { get { return vertex_data.Count / Stride; } }
    public int ByteCount { get { return vertex_data.Count; } }
    public bool IsLoaded { get; private set; }
    public BufferUsageHint UsageHint { get; set; }

    public int VBO { get { return id_vbo; } }
    public int EBO { get { return id_ebo; } }

    private int id_vbo;
    private int id_ebo;

    protected List<byte> vertex_data;
    protected List<ushort> index_data;

    public byte[] Vertices { get { return vertex_data.ToArray(); } }
    public ushort[] Indices { get { return index_data.ToArray(); } }

    public VertexBuffer(VertexFormat format)
    {
        Format = format;
        Stride = (int)format;
        UsageHint = BufferUsageHint.DynamicDraw;

        vertex_data = new List<byte>();
        index_data = new List<ushort>();
    }

    public void Clear()
    {
        vertex_data.Clear();
        index_data.Clear();
    }

    public void SetFormat(VertexFormat format)
    {
        Format = format;
        Stride = (int)format;
        Clear();
    }

    public void Set(byte[] vertices, ushort[] indices)
    {
        Clear();
        vertex_data.AddRange(vertices);
        if (indices != null)
            index_data.AddRange(indices);
    }

    /// <summary>
    /// Load vertex buffer
    /// :: store in memory
    /// </summary>
    public void Load()
    {
        if (IsLoaded) return;

        int count = vertex_data.Count / Stride;
        //VBO
        GL.GenBuffers(1, out id_vbo);
        GL.BindBuffer(BufferTarget.ArrayBuffer, id_vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(count * Stride), vertex_data.ToArray(), UsageHint);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        GL.GenBuffers(1, out id_ebo);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, id_ebo);
        GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(index_data.Count * sizeof(short)), index_data.ToArray(), UsageHint);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

        IsLoaded = true;
    }

    /// <summary>
    /// Reload the buffer data without destroying the buffers pointer to OpenGL
    /// </summary>
    public void Reload()
    {
        if (!IsLoaded) return;

        int count = vertex_data.Count / Stride;

        GL.BindBuffer(BufferTarget.ArrayBuffer, id_vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(count * Stride), IntPtr.Zero, UsageHint);
        GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(count * Stride), vertex_data.ToArray(), UsageHint);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, id_ebo);
        GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(index_data.Count * sizeof(short)), IntPtr.Zero, UsageHint);
        GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(index_data.Count * sizeof(short)), index_data.ToArray(), UsageHint);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
    }

    /// <summary>
    /// Unload vertex buffer from OpenGL
    /// :: release memory
    /// </summary>
    public void Unload()
    {
        if (!IsLoaded) return;

        int count = vertex_data.Count / Stride;

        GL.BindBuffer(BufferTarget.ArrayBuffer, id_vbo);
        GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(count * Stride), IntPtr.Zero, UsageHint);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, id_ebo);
        GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(index_data.Count * sizeof(short)), IntPtr.Zero, UsageHint);

        GL.DeleteBuffers(1, ref id_vbo);
        GL.DeleteBuffers(1, ref id_ebo);

        IsLoaded = false;
    }

    public void Bind(PrimitiveType primitiveType)
    {
        if (!IsLoaded) return;

        GL.BindBuffer(BufferTarget.ArrayBuffer, id_vbo);

        if (Format == VertexFormat.XY)
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.VertexPointer(2, VertexPointerType.Float, Stride, new IntPtr(0));
        }
        else if (Format == VertexFormat.XY_COLOR)
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.ColorArray);
            GL.VertexPointer(2, VertexPointerType.Float, Stride, new IntPtr(0));
            GL.ColorPointer(4, ColorPointerType.UnsignedByte, Stride, new IntPtr(8));
        }
        else if (Format == VertexFormat.XYZ)
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.VertexPointer(3, VertexPointerType.Float, Stride, new IntPtr(0));
        }
        else if (Format == VertexFormat.XYZ_COLOR)
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.ColorArray);
            GL.VertexPointer(3, VertexPointerType.Float, Stride, new IntPtr(0));
            GL.ColorPointer(4, ColorPointerType.UnsignedByte, Stride, new IntPtr(12));
        }
        else if (Format == VertexFormat.XYZ_UV)
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.VertexPointer(3, VertexPointerType.Float, Stride, new IntPtr(0));
            GL.TexCoordPointer(2, TexCoordPointerType.Float, Stride, new IntPtr(12));
        }
        else if (Format == VertexFormat.XYZ_UV_COLOR)
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.EnableClientState(ArrayCap.ColorArray);
            GL.VertexPointer(3, VertexPointerType.Float, Stride, new IntPtr(0));
            GL.TexCoordPointer(2, TexCoordPointerType.Float, Stride, new IntPtr(12));
            GL.ColorPointer(4, ColorPointerType.UnsignedByte, Stride, new IntPtr(20));
        }
        else if (Format == VertexFormat.XYZ_NORMAL_UV)
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.NormalArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.VertexPointer(3, VertexPointerType.Float, Stride, new IntPtr(0));
            GL.NormalPointer(NormalPointerType.Float, Stride, new IntPtr(12));
            GL.TexCoordPointer(2, TexCoordPointerType.Float, Stride, new IntPtr(24));
        }
        else if (Format == VertexFormat.XYZ_NORMAL_UV_COLOR)
        {
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.NormalArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.EnableClientState(ArrayCap.ColorArray);
            GL.VertexPointer(3, VertexPointerType.Float, Stride, new IntPtr(0));
            GL.NormalPointer(NormalPointerType.Float, Stride, new IntPtr(12));
            GL.TexCoordPointer(2, TexCoordPointerType.Float, Stride, new IntPtr(24));
            GL.ColorPointer(4, ColorPointerType.UnsignedByte, Stride, new IntPtr(32));
        }

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, id_ebo);
        GL.DrawElements(primitiveType, index_data.Count, DrawElementsType.UnsignedShort, IntPtr.Zero);

        GL.DisableClientState(ArrayCap.VertexArray);
        GL.DisableClientState(ArrayCap.NormalArray);
        GL.DisableClientState(ArrayCap.TextureCoordArray);
        GL.DisableClientState(ArrayCap.ColorArray);
    }

    public void Dispose()
    {
        Unload();
        Clear();
        vertex_data = null;
        index_data = null;
    }

    public void Join(VertexBuffer buffer)
    {
        int count = vertex_data.Count / Stride;

        //Vertices
        vertex_data.AddRange(buffer.vertex_data.ToArray());

        //Indices
        ushort[] indices = buffer.index_data.ToArray();
        for (int i = 0; i < indices.Length; i++)
        {
            indices[i] += (ushort)count;
        }

        index_data.AddRange(indices);
    }

    /// <summary>
    /// Add indices in order of vertices length,
    /// this is if you dont want to set indices and just index from vertex-index
    /// </summary>
    public void IndexFromLength(bool cw = false)
    {
        int count = vertex_data.Count / Stride;
        ushort[] indices = new ushort[count];
        for (int i = 0; i < count; i++)
        {
            indices[i] = (ushort)i;
        }

        if (cw)
            Array.Reverse(indices);

        AddIndices(indices);
    }

    public void AddIndex(ushort indexA, ushort indexB, ushort indexC)
    {
        index_data.Add(indexA);
        index_data.Add(indexB);
        index_data.Add(indexC);
    }

    public void AddIndices(ushort[] indices)
    {
        index_data.AddRange(indices);
    }

    /// <summary>
    /// XY
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void AddVertex(float x, float y)
    {
        if (Format != VertexFormat.XY)
            throw new FormatException("vertex must be of the same format type as buffer");

        vertex_data.AddRange(BitConverter.GetBytes(x));
        vertex_data.AddRange(BitConverter.GetBytes(y));
    }

    /// <summary>
    /// XY_COLOR
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="color"></param>
    public void AddVertex(float x, float y, uint color)
    {
        if (Format != VertexFormat.XY_COLOR)
            throw new FormatException("vertex must be of the same format type as buffer");

        vertex_data.AddRange(BitConverter.GetBytes(x));
        vertex_data.AddRange(BitConverter.GetBytes(y));
        vertex_data.AddRange(BitConverter.GetBytes(color));
    }

    /// <summary>
    /// XYZ
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    public void AddVertex(float x, float y, float z)
    {
        if (Format != VertexFormat.XYZ)
            throw new FormatException("vertex must be of the same format type as buffer");

        vertex_data.AddRange(BitConverter.GetBytes(x));
        vertex_data.AddRange(BitConverter.GetBytes(y));
        vertex_data.AddRange(BitConverter.GetBytes(z));
    }

    /// <summary>
    /// XYZ_COLOR
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="color"></param>
    public void AddVertex(float x, float y, float z, uint color)
    {
        if (Format != VertexFormat.XYZ_COLOR)
            throw new FormatException("vertex must be of the same format type as buffer");

        vertex_data.AddRange(BitConverter.GetBytes(x));
        vertex_data.AddRange(BitConverter.GetBytes(y));
        vertex_data.AddRange(BitConverter.GetBytes(z));
        vertex_data.AddRange(BitConverter.GetBytes(color));
    }
    /// <summary>
    /// XYZ_UV
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="u"></param>
    /// <param name="v"></param>
    public void AddVertex(float x, float y, float z, float u, float v)
    {
        if (Format != VertexFormat.XYZ_UV)
            throw new FormatException("vertex must be of the same format type as buffer");

        vertex_data.AddRange(BitConverter.GetBytes(x));
        vertex_data.AddRange(BitConverter.GetBytes(y));
        vertex_data.AddRange(BitConverter.GetBytes(z));
        vertex_data.AddRange(BitConverter.GetBytes(u));
        vertex_data.AddRange(BitConverter.GetBytes(v));
    }

    /// <summary>
    /// XYZ_UV_COLOR
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="u"></param>
    /// <param name="v"></param>
    /// <param name="color"></param>
    public void AddVertex(float x, float y, float z, float u, float v, uint color)
    {
        if (Format != VertexFormat.XYZ_UV_COLOR)
            throw new FormatException("vertex must be of the same format type as buffer");

        vertex_data.AddRange(BitConverter.GetBytes(x));
        vertex_data.AddRange(BitConverter.GetBytes(y));
        vertex_data.AddRange(BitConverter.GetBytes(z));
        vertex_data.AddRange(BitConverter.GetBytes(u));
        vertex_data.AddRange(BitConverter.GetBytes(v));
        vertex_data.AddRange(BitConverter.GetBytes(color));
    }

    /// <summary>
    /// XYZ_NORMAL_UV
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="nx"></param>
    /// <param name="ny"></param>
    /// <param name="nz"></param>
    /// <param name="u"></param>
    /// <param name="v"></param>
    public void AddVertex(float x, float y, float z, float nx, float ny, float nz, float u, float v)
    {
        if (Format != VertexFormat.XYZ_NORMAL_UV)
            throw new FormatException("vertex must be of the same format type as buffer");

        vertex_data.AddRange(BitConverter.GetBytes(x));
        vertex_data.AddRange(BitConverter.GetBytes(y));
        vertex_data.AddRange(BitConverter.GetBytes(z));
        vertex_data.AddRange(BitConverter.GetBytes(nx));
        vertex_data.AddRange(BitConverter.GetBytes(ny));
        vertex_data.AddRange(BitConverter.GetBytes(nz));
        vertex_data.AddRange(BitConverter.GetBytes(u));
        vertex_data.AddRange(BitConverter.GetBytes(v));
    }

    /// <summary>
    /// XYZ_NORMAL_UV_COLOR
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <param name="nx"></param>
    /// <param name="ny"></param>
    /// <param name="nz"></param>
    /// <param name="u"></param>
    /// <param name="v"></param>
    /// <param name="color"></param>
    public void AddVertex(float x, float y, float z, float nx, float ny, float nz, float u, float v, uint color)
    {
        if (Format != VertexFormat.XYZ_NORMAL_UV_COLOR)
            throw new FormatException("vertex must be of the same format type as buffer");

        vertex_data.AddRange(BitConverter.GetBytes(x));
        vertex_data.AddRange(BitConverter.GetBytes(y));
        vertex_data.AddRange(BitConverter.GetBytes(z));
        vertex_data.AddRange(BitConverter.GetBytes(nx));
        vertex_data.AddRange(BitConverter.GetBytes(ny));
        vertex_data.AddRange(BitConverter.GetBytes(nz));
        vertex_data.AddRange(BitConverter.GetBytes(u));
        vertex_data.AddRange(BitConverter.GetBytes(v));
        vertex_data.AddRange(BitConverter.GetBytes(color));
    }
}
}