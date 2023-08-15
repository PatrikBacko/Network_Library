﻿using GraphLibrary.Vertices;
using GraphLibrary.Edges;
using GraphLibrary.Graphs.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Data;
using System.Diagnostics;
using GraphLibrary.Graphs.JsonConverters;
using System.Text.Json;
using GraphLibrary.Graphs.Exceptions;

namespace GraphLibrary.Graphs
{
	public class WeightedOrientedGraph<TVertex, TEdge, TWeight> : OrientedGraph<TVertex, TEdge>, IWeightedOrientedGraph<TVertex, TEdge, TWeight>
		where TVertex : WeightedOrientedVertex<TWeight>
		where TEdge : WeightedOrientedEdge<TWeight>
		where TWeight : INumber<TWeight>
	{

		public WeightedOrientedGraph() : base() { }

		public virtual TWeight GetWeight(VertexName vertex) => GetVertex(vertex).Weight;
		public virtual TWeight GetWeight(VertexName vertexOut, VertexName vertexIn) => GetEdge(vertexOut, vertexIn).Weight;

		public virtual WeightedOrientedGraph<TVertex, TEdge, TWeight> ChangeVertexWeight(VertexName vertex, TWeight weight)
		{
			GetVertex(vertex).ChangeWeight(weight);
			return this;
		}

		public virtual WeightedOrientedGraph<TVertex, TEdge, TWeight> ChangeEdgeWeight(VertexName vertexOut, VertexName vertexIn, TWeight weight)
		{
			GetEdge(vertexOut, vertexIn).ChangeWeight(weight);
			return this;
		}

		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> Clear() => (WeightedOrientedGraph<TVertex, TEdge, TWeight>)base.Clear();

		public static new WeightedOrientedGraph<TVertex, TEdge, TWeight> Create() => new WeightedOrientedGraph<TVertex, TEdge, TWeight>();
		public static new WeightedOrientedGraph<TVertex, TEdge, TWeight> Create(IEnumerable<TVertex> vertices, IEnumerable<TEdge> edges)
		{
			var graph = Create();
			graph.AddVertices(vertices);
			graph.AddEdges(edges);
			return graph;
		}

		/// <inheritdoc cref="IWeightedOrientedGraph{TVertex, TEdge, TWeight}.AddVertex(TVertex)"/>
		/// <inheritdoc/>
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> AddVertex(TVertex vertex)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.AddVertex(vertex);
		
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> AddVertices(IEnumerable<TVertex> vertices)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.AddVertices(vertices);

		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> AddEdge(TEdge edge)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.AddEdge(edge);
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> AddEdges(IEnumerable<TEdge> edges)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.AddEdges(edges);

		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveVertex(VertexName vertex)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.RemoveVertex(vertex);
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveVertex(TVertex vertex)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.RemoveVertex(vertex);
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveVertices(IEnumerable<VertexName> vertices)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.RemoveVertices(vertices);
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveVertices(IEnumerable<TVertex> vertices)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.RemoveVertices(vertices);
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveVerticesWith(VertexPredicate<TVertex> vertexPredicate)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.RemoveVerticesWith(vertexPredicate);

		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveEdge(TEdge edge)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.RemoveEdge(edge);
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveEdge(VertexName vertexOut, VertexName vertexIn)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.RemoveEdge(vertexOut, vertexIn);
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveEdges(IEnumerable<TEdge> edges)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.RemoveEdges(edges);
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveEdges(IEnumerable<(VertexName vertexOut, VertexName vertexIn)> edges)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.RemoveEdges(edges);
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> RemoveEdgesWith(EdgePredicate<TEdge> edgePredicate)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.RemoveEdgesWith(edgePredicate);

		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> ApplyToVertices(VertexAction<TVertex> vertexAction)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>)base.ApplyToVertices(vertexAction);
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> ApplyToVerticesWith
		(VertexPredicate<TVertex> vertexPredicate, VertexAction<TVertex> vertexAction)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.ApplyToVerticesWith(vertexPredicate, vertexAction);
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> ApplyToEdges(EdgeAction<TEdge> edgeAction)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.ApplyToEdges(edgeAction);
		public override WeightedOrientedGraph<TVertex, TEdge, TWeight> ApplyToEdgesWith
		(EdgePredicate<TEdge> edgePredicate, EdgeAction<TEdge> edgeAction)
			=> (WeightedOrientedGraph<TVertex, TEdge, TWeight>) base.ApplyToEdgesWith(edgePredicate, edgeAction);

		public override string SerializeToJson()
			=> SerializeToJson(new JsonSerializerOptions()
				{ 
					Converters = { 
						new WeightedOrientedGraphConverter<TVertex, TEdge, TWeight>(),
						new WeightedOrientedVertexConverter<TWeight>(),
						new WeightedOrientedEdgeConverter<TWeight>(),
						new VertexNameConverter() 
					} 
				}
			);

		public override string SerializeToJson(JsonSerializerOptions options)
		{
			try
			{
				return JsonSerializer.Serialize(this, options);
			}
			catch (NotSupportedException e)
			{
				throw new SerializationException("Serialization could not be done because of a problem with Graph", e);
			}

		}

		public static new WeightedOrientedGraph<TVertex, TEdge, TWeight> LoadFromJson(string Path)
			=> DeserializeFromJson(File.ReadAllText(Path));
		public new static WeightedOrientedGraph<TVertex, TEdge, TWeight> DeserializeFromJson(string jsonString)
			=> DeserializeFromJson(jsonString, new JsonSerializerOptions()
				{
					Converters =
						{
							new WeightedOrientedGraphConverter<TVertex, TEdge, TWeight>(),
							new WeightedOrientedVertexConverter<TWeight>(),
							new WeightedOrientedEdgeConverter<TWeight>(),
							new VertexNameConverter()
						}
				});

		public new static WeightedOrientedGraph<TVertex, TEdge, TWeight> DeserializeFromJson(string jsonString, JsonSerializerOptions options)
		{
			try
			{
				return JsonSerializer.Deserialize<WeightedOrientedGraph<TVertex, TEdge, TWeight>>(jsonString, options)
					?? throw new DeserializationException("Deserialization could not be done because null was returned");
			}
			catch (Exception e)
			{
				if (e is JsonException || e is ArgumentNullException || e is NotSupportedException)
					throw new DeserializationException("Deserialization could not be done, check inner exception for more details", e);
				throw;
			}
		}

		public static WeightedOrientedGraph<TVertex, TEdge, TWeight> operator +(WeightedOrientedGraph<TVertex, TEdge, TWeight> graph, TVertex vertex)
			=> graph.AddVertex(vertex);
		public static WeightedOrientedGraph<TVertex, TEdge, TWeight> operator +(WeightedOrientedGraph<TVertex, TEdge, TWeight> graph, TEdge edge)
			=> graph.AddEdge(edge);
		public static WeightedOrientedGraph<TVertex, TEdge, TWeight> operator -(WeightedOrientedGraph<TVertex, TEdge, TWeight> graph, TVertex vertex)
			=> graph.RemoveVertex(vertex);
		public static WeightedOrientedGraph<TVertex, TEdge, TWeight> operator -(WeightedOrientedGraph<TVertex, TEdge, TWeight> graph, TEdge edge)
			=> graph.RemoveEdge(edge);

		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.ApplyToVertices(VertexAction<TVertex> vertexAction)
			=> ApplyToVertices(vertexAction);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.ApplyToVerticesWith
		(VertexPredicate<TVertex> vertexPredicate, VertexAction<TVertex> vertexAction)
			=> ApplyToVerticesWith(vertexPredicate, vertexAction);

		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.ApplyToEdges(EdgeAction<TEdge> edgeAction)
			=> ApplyToEdges(edgeAction);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.ApplyToEdgesWith
		(EdgePredicate<TEdge> edgePredicate, EdgeAction<TEdge> edgeAction)
			=> ApplyToEdgesWith(edgePredicate, edgeAction);

		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.AddVertex(TVertex vertex)
			=> AddVertex(vertex);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.AddVertices(IEnumerable<TVertex> vertices)
			=> AddVertices(vertices);

		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.AddEdge(TEdge edge)
			=> AddEdge(edge);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.AddEdges(IEnumerable<TEdge> edges)
			=> AddEdges(edges);

		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.RemoveVertex(TVertex vertex)
			=> RemoveVertex(vertex);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.RemoveVertices(IEnumerable<TVertex> vertices)
			=> RemoveVertices(vertices);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.RemoveVertex(VertexName vertex)
			=> RemoveVertex(vertex);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.RemoveVertices(IEnumerable<VertexName> vertices)
			=> RemoveVertices(vertices);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.RemoveVerticesWith(VertexPredicate<TVertex> vertexPredicate)
			=> RemoveVerticesWith(vertexPredicate);

		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.RemoveEdge(TEdge edge)
			=> RemoveEdge(edge);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.RemoveEdges(IEnumerable<TEdge> edges)
			=> RemoveEdges(edges);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.RemoveEdge(VertexName vertexOut, VertexName vertexIn)
			=> RemoveEdge(vertexOut, vertexIn);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.RemoveEdges(IEnumerable<(VertexName vertexOut, VertexName vertexIn)> edges)
			=> RemoveEdges(edges);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.RemoveEdgesWith(EdgePredicate<TEdge> edgePredicate)
			=> RemoveEdgesWith(edgePredicate);
		
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.ChangeVertexWeight(VertexName vertex, TWeight weight) 
			=> ChangeVertexWeight(vertex, weight);
		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.ChangeEdgeWeight(VertexName vertexOut, VertexName vertexIn, TWeight weight) 
			=> ChangeEdgeWeight(vertexOut, vertexIn, weight);

		IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.Clear() => Clear();

		static IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.Create() => Create();
		static IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.Create(IEnumerable<TVertex> vertices, IEnumerable<TEdge> edges) => Create(vertices, edges);

		static IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.LoadFromJson(string path)
			=> LoadFromJson(path);
		static IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.DeserializeFromJson(string jsonString)
			=> DeserializeFromJson(jsonString);
		static IWeightedOrientedGraph<TVertex, TEdge, TWeight> IWeightedOrientedGraph<TVertex, TEdge, TWeight>.DeserializeFromJson(string jsonString, JsonSerializerOptions options)
			=> DeserializeFromJson(jsonString, options);


	}
}
