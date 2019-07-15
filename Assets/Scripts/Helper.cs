using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Helper {
	
	private Edge[] BuildManifoldEdges(Mesh mesh) {
		Edge[] edges = BuildEdges(mesh.vertexCount, mesh.triangles);
		
		ArrayList culledEdges = new ArrayList();
		foreach (Edge edge in edges)
		{
			if (edge.faceIndex[0] == edge.faceIndex[1])
			{
				culledEdges.Add(edge);
			}
		}
		
		return culledEdges.ToArray(typeof(Edge)) as Edge[];
	}
	
	private Edge[] BuildEdges(int vertexCount, int[] triangleArray) {
		int maxEdgeCount = triangleArray.Length;
		int[] firstEdge = new int[vertexCount + maxEdgeCount];
		int nextEdge = vertexCount;
		int triangleCount = triangleArray.Length / 3;
		
		for (int a = 0; a < vertexCount; a++)
			firstEdge[a] = -1;
		
		Edge[] edgeArray = new Edge[maxEdgeCount];
		
		int edgeCount = 0;
		for (int a = 0; a < triangleCount; a++)
		{
			int i1 = triangleArray[a * 3 + 2];
			for (int b = 0; b < 3; b++)
			{
				int i2 = triangleArray[a * 3 + b];
				if (i1 < i2)
				{
					Edge newEdge = new Edge();
					newEdge.vertexIndex[0] = i1;
					newEdge.vertexIndex[1] = i2;
					newEdge.faceIndex[0] = a;
					newEdge.faceIndex[1] = a;
					edgeArray[edgeCount] = newEdge;
					
					int edgeIndex = firstEdge[i1];
					if (edgeIndex == -1)
					{
						firstEdge[i1] = edgeCount;
					}
					else
					{
						while (true)
						{
							int index = firstEdge[nextEdge + edgeIndex];
							if (index == -1)
							{
								firstEdge[nextEdge + edgeIndex] = edgeCount;
								break;
							}
							
							edgeIndex = index;
						}
					}
					
					firstEdge[nextEdge + edgeCount] = -1;
					edgeCount++;
				}
				
				i1 = i2;
			}
		}
		
		for (int a = 0; a < triangleCount; a++)
		{
			int i1 = triangleArray[a * 3 + 2];
			for (int b = 0; b < 3; b++)
			{
				int i2 = triangleArray[a * 3 + b];
				if (i1 > i2)
				{
					bool foundEdge = false;
					for (int edgeIndex = firstEdge[i2]; edgeIndex != -1; edgeIndex = firstEdge[nextEdge + edgeIndex])
					{
						Edge edge = edgeArray[edgeIndex];
						if ((edge.vertexIndex[1] == i1) && (edge.faceIndex[0] == edge.faceIndex[1]))
						{
							edgeArray[edgeIndex].faceIndex[1] = a;
							foundEdge = true;
							break;
						}
					}
					
					if (!foundEdge)
					{
						Edge newEdge = new Edge();
						newEdge.vertexIndex[0] = i1;
						newEdge.vertexIndex[1] = i2;
						newEdge.faceIndex[0] = a;
						newEdge.faceIndex[1] = a;
						edgeArray[edgeCount] = newEdge;
						edgeCount++;
					}
				}
				
				i1 = i2;
			}
		}
		
		Edge[] compactedEdges = new Edge[edgeCount];
		for (int e = 0; e < edgeCount; e++)
			compactedEdges[e] = edgeArray[e];
		
		return compactedEdges;
	}
	
	private class Edge {
		public int[] vertexIndex = new int[2];
		public int[] faceIndex = new int[2];
	}
	
	private void ClearTexture (Texture2D tex) {
		Color[] colors = new Color[tex.width*tex.height];
		for (int i = 0; i < tex.width*tex.height; i++) {
			colors[i] = Color.clear;
		}
		tex.SetPixels(colors);
	}
	
	private bool pointInTexture (int x, int y, Texture2D tex) {
		if (x <= 0 || x >= tex.width)
			return false;
		else if (y <= 0 || y >= tex.height)
			return false;
		else
			return true;
	}
	
	private void DrawPoint(Texture2D tex, Color col, int x, int y, int width) {
		tex.SetPixel(x, y, col);
		for (int i = 1; i <= width/2; i++) {
			tex.SetPixel(x + i, y, col);
			tex.SetPixel(x - i, y, col);
			//tex.SetPixel(x, y + 1, col);
			//tex.SetPixel(x, y - 1, col);
		}
		for (int i = 1; i <= width/2; i++) {
			//tex.SetPixel(x + i, y, col);
			//tex.SetPixel(x - i, y, col);
			tex.SetPixel(x, y + i, col);
			tex.SetPixel(x, y - i, col);
		}
	}
	
	private void DrawLine(Texture2D tex, int x0, int y0, int x1, int y1, Color col, int width)
	{
		int dy = (int)(y1-y0);
		int dx = (int)(x1-x0);
		int stepx, stepy;
		
		if (dy < 0) {dy = -dy; stepy = -1;}
		else {stepy = 1;}
		if (dx < 0) {dx = -dx; stepx = -1;}
		else {stepx = 1;}
		dy <<= 1;
		dx <<= 1;
		
		float fraction = 0;
		
		DrawPoint(tex, col, x0, y0, width);
		
		if (dx > dy) {
			fraction = dy - (dx >> 1);
			while (Mathf.Abs(x0 - x1) > 1) {
				if (fraction >= 0) {
					y0 += stepy;
					fraction -= dx;
				}
				x0 += stepx;
				fraction += dy;
				
				DrawPoint(tex, col, x0, y0, width);
			}
		}
		else {
			fraction = dx - (dy >> 1);
			while (Mathf.Abs(y0 - y1) > 1) {
				if (fraction >= 0) {
					x0 += stepx;
					fraction -= dy;
				}
				y0 += stepy;
				fraction += dx;
				
				DrawPoint(tex, col, x0, y0, width);
			}
		}
	}
	
	private int[] MergeVertices (int[] triangles, Vector3[] vertices) {
		int[] trian = new int[triangles.Length];
		Array.Copy(triangles, trian, triangles.Length);
		trian[0] = triangles[0];
		for (int i = 0; i < triangles.Length-1; i++) {
			for (int j = i+1; j < triangles.Length; j++) {
				if ((Vector3.Distance(vertices[triangles[i]], vertices[triangles[j]]) < 0.01f) && (
					triangles[i] != triangles[j])) {
					trian[j] = triangles[i];
				}
				else {
					trian[j] = trian[j];
				}
			}
		}
		return trian;
	}
	
	public Texture2D DrawTexture (GameObject gameObject) {
		
		Mesh m = gameObject.GetComponent<MeshFilter>().mesh;
		
		int[] tr = MergeVertices(m.triangles, m.vertices);
		m.triangles = tr;
		
		int[] tr2 = MergeVertices(m.triangles, m.vertices);
		m.triangles = tr2;
		
		Edge[] outline = BuildManifoldEdges(m);
		
		Vector3 sizeObject = gameObject.GetComponent<Renderer>().bounds.size;
		Vector3 centerBounds = gameObject.GetComponent<Renderer>().bounds.center;
		Bounds meshBounds = gameObject.GetComponent<Renderer>().bounds;
		float textureHeight1 = sizeObject.z * 128;
		float textureWidth1 = sizeObject.x * 128;
		textureHeight1 = ((int)(textureHeight1 / 8) + 1) * 8;
		textureWidth1 = ((int)(textureWidth1 / 8) + 1) * 8;
		float maxDimenshion;
		if (textureHeight1 > textureWidth1) {
			maxDimenshion = textureHeight1;
		}
		else {
			maxDimenshion = textureWidth1;
		}
		
		float textureHeight = 0;
		float textureWidth = 0;
		
		for (int i = 6; i < 12; i++) {
			if (maxDimenshion < Math.Pow(2, i)) {
				textureHeight = (int)Math.Pow(2, i);
				textureWidth = (int)Math.Pow(2, i);
				break;
			}
		}
		
		Vector2 textureCenter = new Vector2((int)(textureWidth / 2), (int)(textureHeight / 2));
		
		Vector2[] uvVert = m.uv;
		Vector3[] vert = m.vertices;
		
		float xRatio = 0.5f / meshBounds.extents.x * (textureWidth1 / textureWidth);
		float yRatio = 0.5f / meshBounds.extents.z * (textureHeight1 / textureHeight);

		//---

		for (int i = 0; i < uvVert.Length; i++) {
			
			float diffX = vert[i].x - centerBounds.x;
			float diffY = vert[i].z - centerBounds.z;
			
			uvVert[i].x = 0.5f + diffX * xRatio;
			uvVert[i].y = 0.5f + diffY * yRatio;
			
		}
		
		m.uv = uvVert;
		
		Texture2D texture2d = new Texture2D((int)textureWidth, (int)textureHeight);
		ClearTexture(texture2d);
		
		float xRatio1 = textureWidth1 / 2 / meshBounds.extents.x;
		float yRatio1 = textureHeight1 / 2 / meshBounds.extents.z;
		
		for (int i = 0; i < outline.Length; i++) {
			float diffX1 = vert[outline[i].vertexIndex[0]].x - centerBounds.x;
			float diffY1 = vert[outline[i].vertexIndex[0]].z - centerBounds.z;
			
			float x1 = textureCenter.x + diffX1 * xRatio1;
			float y1 = textureCenter.y + diffY1 * yRatio1;
			
			float diffX2 = vert[outline[i].vertexIndex[1]].x - centerBounds.x;
			float diffY2 = vert[outline[i].vertexIndex[1]].z - centerBounds.z;
			
			float x2 = textureCenter.x + diffX2 * xRatio1;
			float y2 = textureCenter.y + diffY2 * yRatio1;
			
			DrawLine(texture2d, (int)x1, (int)y1, (int)x2, (int)y2, Color.black, 7);
		}

		//---
		
		/*for (int i = 0; i < uvVert.Length; i++) {

			float diffX = vert[i].x - centerBounds.x;
			float diffY = vert[i].z - centerBounds.z;
			
			uvVert[i].x = 0.5f + diffX * xRatio;
			uvVert[i].y = 0.5f + diffY * yRatio;
			
		}
		
		m.uv = uvVert;
		
		Texture2D texture2d = new Texture2D((int)textureWidth, (int)textureHeight);
		ClearTexture(texture2d);
		
		float xRatio1 = textureWidth1 / 2 / meshBounds.extents.x;
		float yRatio1 = textureHeight1 / 2 / meshBounds.extents.z;
		
		for (int i = 0; i < outline.Length; i++) {
			float diffX1 = vert[outline[i].vertexIndex[0]].x - centerBounds.x;
			float diffY1 = vert[outline[i].vertexIndex[0]].z - centerBounds.z;
			
			float x1 = textureCenter.x + diffX1 * xRatio1;
			float y1 = textureCenter.y + diffY1 * yRatio1;
			
			float diffX2 = vert[outline[i].vertexIndex[1]].x - centerBounds.x;
			float diffY2 = vert[outline[i].vertexIndex[1]].z - centerBounds.z;
			
			float x2 = textureCenter.x + diffX2 * xRatio1;
			float y2 = textureCenter.y + diffY2 * yRatio1;
			
			DrawLine(texture2d, (int)x1, (int)y1, (int)x2, (int)y2, Color.red, 5);
		}*/
		
		texture2d.Apply();
		
		return texture2d;
		
	}

}
