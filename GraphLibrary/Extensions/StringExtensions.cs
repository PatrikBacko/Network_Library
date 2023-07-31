﻿using System.Runtime.CompilerServices;
using GraphLibrary.Edges;
using GraphLibrary.Vertices;
using System.Numerics;

namespace GraphLibrary.Extensions.StringExtensions
{
    static public class StringExtensions
    {
        static public VertexName ToVertexName(this string srt)
        {
            return new VertexName(srt);
        }

        static public OrientedVertex ToOrientedVertex(this string srt) {
            return new OrientedVertex(new VertexName(srt));
        }

        static public OrientedEdge ToOrientedEdge(this string vertexOut, string vertexIn) { 
            return new OrientedEdge(new VertexName(vertexOut), new VertexName(vertexIn));
        }

        static public WeightedOrientedEdge<TWeight> ToWeightedOrientedEdge<TWeight>(this string vertexOut, string vertexIn, TWeight weight) 
			where TWeight : INumber<TWeight>{
			return new WeightedOrientedEdge<TWeight>(new VertexName(vertexOut), new VertexName(vertexIn), weight);
		}
        static public WeightedOrientedVertex<TWeight> ToWeightedOrientedVertex<TWeight>(this string vertex, TWeight weight)
            where TWeight : INumber<TWeight>{
            return new WeightedOrientedVertex<TWeight>(new VertexName(vertex), weight);
        }

    }
}
