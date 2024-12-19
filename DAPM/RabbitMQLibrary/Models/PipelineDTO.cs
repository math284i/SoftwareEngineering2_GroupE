using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * All new changes are made by:
 * @Author: s20416 and s204178
 */

namespace RabbitMQLibrary.Models
{
    public class Resource
    {
        public Guid OrganizationId { get; set; }
        public Guid RepositoryId { get; set; }
        public Guid? ResourceId { get; set; }
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? File { get; set; }
    }

    public class Organization
    {
        public Guid Id { get; set; }
        public string Domain { get; set; }
        public string Name { get; set; }
    }
   
    public class Algorithm
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public Guid RepositoryId { get; set; }
        public string Type { get; set; }
        public string? File { get; set; }
    }

    public class PipelineRepository
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public string Name { get; set; }
    }
   
    public class Handle
    {
        public string Id { get; set; }
        public string? Type { get; set; }
        
    }
    
    public class InstantiationData
    {
        public Resource? Resource { get; set; }
        public Organization? Organization { get; set; }
        public Algorithm? Algorithm { get; set; }
        public PipelineRepository? Repository { get; set; }
    }
    public class TemplateData
    {
        public IEnumerable<Handle> SourceHandles { get; set; }
        public IEnumerable<Handle> TargetHandles { get; set; }
        public string? Hint { get; set; }
    }
    public class Edge
    {
        public string SourceHandle { get; set; }
        public string TargetHandle { get; set; }
        public string? Source { get; set; }
        public string? Target { get; set; }
        public string? Type { get; set; }
        public string? Id { get; set; }
        public EdgeData? Data { get; set; }
    }

    public class EdgeData
    {
        public string Filename { get; set; }
    }

    public class NodePosition
    {
        public float? X { get; set; }
        public float? Y { get; set; }
    }

    public class Style
    {
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? ZIndex { get; set; }
    }

    public class NodeData
    {
        public string? Label { get; set; }
        public TemplateData TemplateData { get; set; }
        public InstantiationData InstantiationData { get; set; }
    }

    public class Node
    {
        public string Id { get; set;}
        public string Type { get; set; }
        public NodePosition? Position { get; set; }
        public NodePosition? PositionAbsolute { get; set; }
        public NodeData Data { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string? ParentNode { get; set; }
        public Style? Style { get; set; }
        public string? Extent { get; set; }
        public string? ClassName { get; set; }
    }
    public class Pipeline
    {
        public IEnumerable<Node> Nodes { get; set; }
        public IEnumerable<Edge> Edges { get; set; }
        public int? Timestamp { get; set; }
    }

    public class PipelineDTO
    {
        public Guid OrganizationId { get; set; }
        public Guid RepositoryId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Pipeline? Pipeline { get; set; }
        public int? Timestamp { get; set; }
       
    }
}
