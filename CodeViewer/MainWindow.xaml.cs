using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.SyntaxEditor.Tagging.Taggers;

namespace CodeViewer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			string programText = File.ReadAllText(@"C:\Users\qwert\code\reporter-2\Reporter2\UI\OrderManagement\OrderManagementHelper2.cs");
			//SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);
			//CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

			var tree = CSharpSyntaxTree.ParseText(programText);
			var walker = new CustomWalker();
			walker.tv1 = tv1;
			walker.Visit(tree.GetRoot());
			tv1.ExpandAll();
			tv1.SelectionChanged += Tv1_SelectionChanged;
			var cSharptagger = new CSharpTagger(this.txtCode);
			this.txtCode.TaggersRegistry.RegisterTagger(cSharptagger);
		}

		private void Tv1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			string code = ((TreeViewSyntaxNode)tv1.SelectedItem).SyntaxNode.ToString();

			txtCode.Document = new Telerik.Windows.SyntaxEditor.Core.Text.TextDocument(code);
			txtCode.Commands.SelectAllCommand.Execute(null);
			txtCode.Commands.UnindentCommand.Execute(null);
			txtCode.Commands.UnindentCommand.Execute(null);
			
		}
	}

	public class CustomWalker : CSharpSyntaxWalker
	{
		public RadTreeView tv1 { get; set; }
		public List<TreeViewSyntaxNode> nodelist = new List<TreeViewSyntaxNode>();

		private TreeViewSyntaxNode GetContainingNode(SyntaxNode current)
		{
			for (int x = nodelist.Count - 1; x > -1; x--)
			{
				var lastnode = nodelist[x];
				if (lastnode.SyntaxNode.Contains(current))
				{
					return lastnode;
				}

			}
			return null;
		}


		public override void Visit(SyntaxNode node)
		{

			if (node.Kind() == SyntaxKind.CompilationUnit)
			{
				TreeViewSyntaxNode tn = new TreeViewSyntaxNode();
				tn.Header = node.Kind().ToString();
				tn.SyntaxNode = node;
				tv1.Items.Add(tn);
				nodelist.Add(tn);
			}


			if (node.Kind() == SyntaxKind.NamespaceDeclaration)
			{
				TreeViewSyntaxNode tn = new TreeViewSyntaxNode();
				var namesp = ((NamespaceDeclarationSyntax)node);
				tn.Header = namesp.Name.ToString();
				tn.SyntaxNode = node;
				TreeViewSyntaxNode containingNode = GetContainingNode(node);
				if (containingNode != null)
				{
					containingNode.Items.Add(tn);
				}
				else
				{
					tv1.Items.Add(tn);
				}

				nodelist.Add(tn);
			}

			if (node.Kind() == SyntaxKind.ClassDeclaration)
			{
				TreeViewSyntaxNode tn = new TreeViewSyntaxNode();
				var namesp = ((ClassDeclarationSyntax)node);
				tn.Header = namesp.Identifier.Text;
				tn.SyntaxNode = node;
				TreeViewSyntaxNode containingNode = GetContainingNode(node);
				if (containingNode != null)
				{
					containingNode.Items.Add(tn);
				}
				else
				{
					tv1.Items.Add(tn);
				}

				nodelist.Add(tn);
			}


			if (node.Kind() == SyntaxKind.MethodDeclaration)
			{
				TreeViewSyntaxNode tn = new TreeViewSyntaxNode();
				var namesp = ((MethodDeclarationSyntax)node);
				tn.Header = namesp.Identifier.Text;
				tn.SyntaxNode = node;
				TreeViewSyntaxNode containingNode = GetContainingNode(node);
				if (containingNode != null)
				{
					containingNode.Items.Add(tn);
				}
				else
				{
					tv1.Items.Add(tn);
				}

				nodelist.Add(tn);
			}

			if (node.Kind() == SyntaxKind.FieldDeclaration)
			{
				TreeViewSyntaxNode tn = new TreeViewSyntaxNode();
				var namesp = ((FieldDeclarationSyntax)node);
				tn.Header = namesp.Declaration.Variables[0].Identifier.Text;
				tn.SyntaxNode = node;
				TreeViewSyntaxNode containingNode = GetContainingNode(node);
				if (containingNode != null)
				{
					containingNode.Items.Add(tn);
				}
				else
				{
					tv1.Items.Add(tn);
				}

				nodelist.Add(tn);
			}


			if (node.Kind() == SyntaxKind.PropertyDeclaration)
			{
				TreeViewSyntaxNode tn = new TreeViewSyntaxNode();
				var namesp = ((PropertyDeclarationSyntax)node);
				tn.Header = namesp.Identifier.Text;
				tn.SyntaxNode = node;
				TreeViewSyntaxNode containingNode = GetContainingNode(node);
				if (containingNode != null)
				{
					containingNode.Items.Add(tn);
				}
				else
				{
					tv1.Items.Add(tn);
				}

				nodelist.Add(tn);
			}


			if (node.Kind() == SyntaxKind.EnumDeclaration)
			{
				TreeViewSyntaxNode tn = new TreeViewSyntaxNode();
				var namesp = ((EnumDeclarationSyntax)node);
				tn.Header = namesp.Identifier.Text;
				tn.SyntaxNode = node;
				TreeViewSyntaxNode containingNode = GetContainingNode(node);
				if (containingNode != null)
				{
					containingNode.Items.Add(tn);
				}
				else
				{
					tv1.Items.Add(tn);
				}

				nodelist.Add(tn);
			}

			//Console.WriteLine(node.Kind());
			base.Visit(node);

		}


	}

	public class TreeViewSyntaxNode: RadTreeViewItem
	{
		public SyntaxNode SyntaxNode { get; set; }
	}
}
