using System.Diagnostics;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MinCutMaxFlow
{
    public partial class MinCutMaxFlow : Form
    {
        string filePath = string.Empty;
        List<List<int>> inputData = new List<List<int>>();

        // for max flow
        List<Tuple<int, int, int>> maxFlowValues = new List<Tuple<int, int, int>>();
        //List<Tuple<int, int>> minCutEdges = new List<Tuple<int, int>>();

        // for min cost
        int minCost = 0;
        List<Tuple<int, int, int>> minCostMaxFlowValues = new List<Tuple<int, int, int>>();
        //List<Tuple<int, int>> minCut = new List<Tuple<int, int>>();

        int numNodes;
        int numArcs;
        int sourceNode;
        int sinkNode;

        int output_cnt = 0;

        public MinCutMaxFlow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filePath = Path.Combine(Application.StartupPath, "example_graph.txt");
            inputData = getInputData(filePath);
            DotLoad();
        }

        private void addFileButton_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();                           // Creating an instance of the OpenFileDialog

            openFileDialog.Title = "Select a File";                                         // Setting the properties of the OpenFileDialog
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog.InitialDirectory = "C:\\";                                       // Setting the initial directory

            DialogResult result = openFileDialog.ShowDialog();                              // Showing the OpenFileDialog and get the result

            if (result == DialogResult.OK)                                                  // Check if the user clicked the OK button
            {
                maxFlowNameLabel.Visible = false;                       // "Clear" results (just makes them invisible)
                maxFlowLabel.Visible = false;
                flowValuesLabel.Visible = false;
                flowValuesNameLabel.Visible = false;

                minCutLabel.Visible = false;
                minCutValuesNameLabel.Visible = false;

                minCostLabel.Visible = false;
                minCostNameLabel.Visible = false;
                minCostMaxFlowLabel.Visible = false;
                minCostMaxFlowNameLabel.Visible = false;
                minCostFlowValuesLabel.Visible = false;
                minCostFlowValuesNameLabel.Visible = false;

                filePath = openFileDialog.FileName;                                         // Get the selected file path
                inputData = getInputData(filePath);
                DotLoad();
            }
        }

        private void DotLoad()  // Generating the DOT file
        {
            string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "output" + output_cnt.ToString() + ".png");    //Setting the path to the output file to be the same as the input file


            // Run the dot command
            ProcessStartInfo startInfoDot = new ProcessStartInfo             // Arguments for DOT command
            {
                FileName = "dot",
                Arguments = $"-Tpng -o \"{outputFilePath}\"",
                UseShellExecute = false,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process dotProcess = Process.Start(startInfoDot))
            {
                dotProcess.StandardInput.Write(generateDotCodeFromRows());         // Giving the DOT code generated with GenerateDotCodeFromRows as inputs to DOT command
                dotProcess.StandardInput.Close();
                dotProcess.WaitForExit();
            }

            DotLoadImage();
        }

        private void DotLoadImage()
        {
            string outputFilePath = Path.Combine(Path.GetDirectoryName(filePath), "output" + output_cnt.ToString() + ".png");

            pictureBoxGraph.Image = Image.FromFile(outputFilePath);
            pictureBoxGraph.SizeMode = PictureBoxSizeMode.AutoSize;                         // Showing generated image

            output_cnt += 1;
        }

        public string generateDotCodeFromRows()                                                 //Generate the DOT code from a comma separated csv specified in the project description
        {
            // Generate DOT code from dataList
            string dotCode = "digraph G {\n";
            bool isFirstLine = true;

            foreach (List<int> row in inputData)
            {
                if (isFirstLine)                                                                //The first line contains different values than the rest
                {
                    isFirstLine = false;
                    numNodes = row[0];
                    numArcs = row[1];
                    sourceNode = row[2];
                    sinkNode = row[3];
                    continue;
                }

                int source = row[0];
                int target = row[1];
                int capacity = row[2];
                int cost = row[3];

                string costString = cost.ToString();
                string[] split = costString.Select(c => c.ToString()).ToArray();                    //Split the last item in a row as they are the decimal cost values

                dotCode += $"    {source} -> {target} [label=\"{capacity} ({split[0]}.{split[1]})\"];\n";
            }
            dotCode += "}";

            return dotCode;

        }

        public List<List<int>> getInputData(string filePath)
        {
            string[] fileLines = File.ReadAllLines(filePath);          // Read each line as strings
            List<List<int>> inputData = new List<List<int>>();

            foreach (string line in fileLines)                         // For each line, split the line at ",", and store the resulting values in "values"
            {
                string[] values = line.Split(',');
                List<int> inputRow = new List<int>();

                foreach (string value in values)                      //For each value in values, if it can be converted to int, add it to the current inputRow
                {
                    int intValue;
                    if (int.TryParse(value, out intValue))
                    {
                        inputRow.Add(intValue);
                    }
                    else
                    {
                        MessageBox.Show("Incorrect input data, use integers only please!");
                    }
                }
                inputData.Add(inputRow);
            }

            return inputData;
        }


        private void maxFlowButton_Click(object sender, EventArgs e)           // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        {
            maxFlowValues.Clear();
            int maxFlow = CalculateMaxFlow(inputData);
            maxFlowLabel.Text = maxFlow.ToString();

            StringBuilder sb = new StringBuilder();
            foreach (var flowValue in maxFlowValues)
            {
                sb.AppendLine($"From: {flowValue.Item1}, To: {flowValue.Item2}, Flow: {flowValue.Item3}");
            }

            flowValuesLabel.Text = sb.ToString();

            maxFlowNameLabel.Visible = true;                      // Clear results
            maxFlowLabel.Visible = true;
            flowValuesLabel.Visible = true;
            flowValuesNameLabel.Visible = true;

        }

        private int CalculateMaxFlow(List<List<int>> data)
        {
            int[,] graph = new int[numNodes, numNodes];                              // Initializing an adjacency matrix to represent the graph of size numNodes X numNodes

            for (int i = 1; i <= numArcs; i++)                                       // Filling the adjacency matrix with the appropriate capacities of the arcs
            {
                List<int> arcParts = data[i];
                int fromNode = arcParts[0];
                int toNode = arcParts[1];
                int capacity = arcParts[2];

                graph[fromNode, toNode] = capacity;
            }

            // Performs the Edmonds-Karp algorithm to find the maximum flow
            int[] parent = new int[numNodes];
            int maxFlow = 0;

            for (int i = 0; i < numNodes - 1; i++)                                    // Complete the algo with numNodes-1 times
            {
                Array.Fill(parent, -1);                                              // Initialize the parent array for each iteration

                while (BFS(graph, sourceNode, sinkNode, parent))                     // While there are still augmenting paths
                {
                    int bottleneck = int.MaxValue;                                   // Find the bottleneck capacity in the augmenting path
                    for (int v = sinkNode; v != sourceNode; v = parent[v])           // Starting from the sink backwards, travel through the found path
                    {
                        int u = parent[v];
                        bottleneck = Math.Min(bottleneck, graph[u, v]);              // update the bottleneck value
                    }

                    for (int v = sinkNode; v != sourceNode; v = parent[v])                // Updating the residual capacities and flow along the augmenting path
                    {
                        int u = parent[v];
                        graph[u, v] -= bottleneck;                                       // Reduce the capacity of the forward edge in the augmenting path by the bottleneck value
                        graph[v, u] += bottleneck;                                       // Increase the capacity of the backward edge in the augmenting path by the bottleneck value

                        maxFlowValues.Add(Tuple.Create(u, v, bottleneck));                     // Add the flow value to the flow list
                    }

                    maxFlow += bottleneck;                                               // Increase the max flow by the bottleneck capacity, as the max flow is equal to the sum of bottleneck capacities
                }
            }

            //StringBuilder sb = new StringBuilder();                       //Print the residual graph for debugging only!
            //sb.AppendLine("Residual Graph:");
            //for (int i = 0; i < numNodes; i++)
            //{
            //    for (int j = 0; j < numNodes; j++)
            //    {
            //        sb.Append(graph[i, j] + " ");
            //    }
            //    sb.AppendLine();
            //}
            //residualGraphLabel.Text = sb.ToString();
            //residualGraphLabel.Visible = true;

            return maxFlow;
        }

        private bool BFS(int[,] graph, int source, int sink, int[] parent)                       // finds an augmenting path with breadth-first search
        {
            bool[] visited = new bool[numNodes];                                        // a list of lenght numNodes, for storing which nodes have been visited

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(source);                                                      // start with the source 
            visited[source] = true;

            while (queue.Count > 0)
            {
                int v = queue.Dequeue();

                for (int u = 0; u < numNodes; u++)
                {
                    if (!visited[u] && graph[v, u] > 0)                 // for each nextNode that has been not visited yet, and if it is a neighbour of (current) node with positive capacity, add them to the queue
                    {
                        queue.Enqueue(u);
                        visited[u] = true;
                        parent[u] = v;

                        if (u == sink)
                            return true;                                               // if the sink is reached then we have found an augmenting path
                    }
                }
            }

            return false;                                                              // if the sink node is not reached and there are no more nodes to process, we have completed the BFS and there are no more augmenting paths to find
        }




        private void minCutButton_Click(object sender, EventArgs e)
        {
            minCutLabel.Text = "";
            List<Tuple<int, int>> minCuts = CalculateMinCut(inputData);

            StringBuilder sb = new StringBuilder();
            foreach (var minCut in minCuts)
            {
                sb.AppendLine($"From: {minCut.Item1}, To: {minCut.Item2}");
            }

            minCutLabel.Text = sb.ToString();

            minCutValuesNameLabel.Visible = true;
            minCutLabel.Visible = true;


        }

        private List<Tuple<int, int>> CalculateMinCut(List<List<int>> data)
        {
            int[,] graph = new int[numNodes, numNodes];                                  // Initializing an adjacency matrix to represent the graph of size numNodes X numNodes

            for (int i = 1; i <= numArcs; i++)                                                           // Filling the adjacency matrix with the appropriate capacities of the arcs
            {
                List<int> arcParts = data[i];
                int fromNode = arcParts[0];
                int toNode = arcParts[1];
                int capacity = arcParts[2];

                graph[fromNode, toNode] = capacity;
            }


            int[,] rGraph = new int[numNodes, numNodes];                            // the original graph will be also needed when finding the min cut edges, so the Edmond-Karp algorithm will run on rGraph

            for (int i = 0; i < numNodes; i++)
            {
                for (int j = 0; j < numNodes; j++)
                {
                    rGraph[i, j] = graph[i, j];
                }
            }


            // Ford-Fulkerson algorithm to find the maximum flow
            int[] parent = new int[numNodes];
            int maxFlow = 0;

            for (int i = 0; i < numNodes - 1; i++)                                    // Complete the algo with numNodes-1 times
            {
                Array.Fill(parent, -1);                                              // Initialize the parent array for each iteration

                while (BFS(rGraph, sourceNode, sinkNode, parent))                     // While there are still augmenting paths
                {
                    int bottleneck = int.MaxValue;                                   // Find the bottleneck capacity in the augmenting path
                    for (int v = sinkNode; v != sourceNode; v = parent[v])           // Starting from the sink backwards, travel through the found path
                    {
                        int u = parent[v];
                        bottleneck = Math.Min(bottleneck, rGraph[u, v]);              // update the bottleneck value
                    }

                    for (int v = sinkNode; v != sourceNode; v = parent[v])                // Updating the residual capacities and flow along the augmenting path
                    {
                        int u = parent[v];
                        rGraph[u, v] -= bottleneck;                                       // Reduce the capacity of the forward edge in the augmenting path by the bottleneck value
                        rGraph[v, u] += bottleneck;                                       // Increase the capacity of the backward edge in the augmenting path by the bottleneck value

                        maxFlowValues.Add(Tuple.Create(u, v, bottleneck));                     // Add the flow value to the flow list
                    }

                    maxFlow += bottleneck;
                }
            }

            bool[] visited = new bool[numNodes];                                            // Find the nodes reachable from the source (S) in the residual graph
            DFS(rGraph, sourceNode, visited);
            // We visit all nodes we can from the source. The edges that will be in the min cut are the ones between node(u) that we could visit and node(v) that we could not in the residual graph, but only for those nodes that had an edge in the original graph
            List<Tuple<int, int>> cut = new List<Tuple<int, int>>();
            for (int u = 0; u < numNodes; u++)
            {
                for (int v = 0; v < numNodes; v++)
                {
                    if (visited[u] && !visited[v] && graph[u, v] > 0)
                    {
                        cut.Add(new Tuple<int, int>(u, v));
                    }
                }
            }
            return cut;
        }

        private void DFS(int[,] graph, int node, bool[] visited)                                // Depth first search algorithm
        {
            visited[node] = true;

            int numNodes = graph.GetLength(0);
            for (int v = 0; v < numNodes; v++)
            {
                if (!visited[v] && graph[node, v] > 0)
                {
                    DFS(graph, v, visited);
                }
            }
        }

        private void minCostMaxFlowButton_Click(object sender, EventArgs e)
        {
            double maxFlow = CalculateMinCostMaxFlow(inputData);

            minCostMaxFlowLabel.Text = maxFlow.ToString();
            minCostLabel.Text = (minCost / 100.0).ToString();


            StringBuilder sb = new StringBuilder();
            foreach (var flow in minCostMaxFlowValues)
            {
                sb.AppendLine($"From: {flow.Item1}, To: {flow.Item2}, Flow: {flow.Item3}");
            }

            minCostFlowValuesLabel.Text = sb.ToString();

            minCostLabel.Visible = true;
            minCostNameLabel.Visible = true;
            minCostMaxFlowLabel.Visible = true;
            minCostMaxFlowNameLabel.Visible = true;
            minCostFlowValuesLabel.Visible = true;
            minCostFlowValuesNameLabel.Visible = true;


        }


        private int CalculateMinCostMaxFlow(List<List<int>> data)
        {
            int[,] graph = new int[numNodes, numNodes];
            int[,] cost = new int[numNodes, numNodes];
            int[,] rgraph = new int[numNodes, numNodes];

            for (int i = 1; i <= numArcs; i++)
            {
                List<int> arcParts = data[i];
                int fromNode = arcParts[0];
                int toNode = arcParts[1];
                int capacity = arcParts[2];
                int arcCost = arcParts[3];

                graph[fromNode, toNode] = capacity;
                cost[fromNode, toNode] = arcCost;
                rgraph[fromNode, toNode] = capacity;
            }

            int[] parent = new int[numNodes];
            int[] distance = new int[numNodes];
            int maxFlow = 0;

            while (BellmanFord(rgraph, cost, parent, distance))
            {
                int bottleneck = int.MaxValue;
                for (int v = sinkNode; v != sourceNode; v = parent[v])
                {
                    int u = parent[v];
                    bottleneck = Math.Min(bottleneck, rgraph[u, v]);
                }

                for (int v = sinkNode; v != sourceNode; v = parent[v])
                {
                    int u = parent[v];
                    rgraph[u, v] -= bottleneck;
                    rgraph[v, u] += bottleneck;
                    minCost += bottleneck * cost[u, v];                                            // Calculating the min cost
                    minCostMaxFlowValues.Add(Tuple.Create(u, v, bottleneck));
                }

                maxFlow += bottleneck;
            }


            //bool[] visited = new bool[numNodes];
            //DFS(rgraph, sourceNode, visited);


            //for (int u = 0; u < numNodes; u++)
            //{
            //    for (int v = 0; v < numNodes; v++)
            //    {
            //        if (visited[u] && !visited[v] && graph[u, v] > 0)
            //        {
            //            minCut.Add(Tuple.Create(u, v));
            //        }
            //    }
            //}

            return maxFlow;
        }

        private bool BellmanFord(int[,] graph, int[,] cost, int[] parent, int[] distance)
        {
            Array.Fill(distance, int.MaxValue);
            Array.Fill(parent, -1);
            distance[sourceNode] = 0;


            for (int i = 0; i < numNodes - 1; i++)                                              // Bellman-Ford algorithm has to run numNodes-1 times
            {
                bool updated = false;
                for (int u = 0; u < numNodes; u++)
                {
                    for (int v = 0; v < numNodes; v++)
                    {
                        //MessageBox.Show($"u: {u}, v: {v}, rgraph: {graph[u, v]}, distu: {distance[u]}, cost: {cost[u, v]}, distv: {distance[v]}");
                        if (graph[u, v] > 0 && distance[u] != int.MaxValue && distance[u] + cost[u, v] < distance[v])                                               // if edge exists between two nodes v and u, and the distance of u is not "infinity"(reachable), and the distance of u + the cost of u to v is less than the current distance of v, update the cost            
                        {
                            distance[v] = distance[u] + cost[u, v];
                            parent[v] = u;
                            updated = true;
                            //MessageBox.Show("Updated!");
                        }
                    }
                }

                if (!updated)                        //If the distances are not updating, end the loop
                {
                    break;
                }

            }

            for (int u = 0; u < numNodes; u++)
            {
                for (int v = 0; v < numNodes; v++)
                {
                    if (graph[u, v] > 0 && distance[u] != int.MaxValue && distance[u] + cost[u, v] < distance[v])
                    {
                        MessageBox.Show("Negative cycle detected!");
                        return false;
                    }
                }
            }


            if (distance[sinkNode] == int.MaxValue)                        // Check if the sink node is reachable, if it is not, terminate the algorithm
            {
                return false;
            }

            return true;
        }




    }


}