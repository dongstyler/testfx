﻿// Copyright (c) Microsoft Corporation. All rights reserved.

namespace Microsoft.MSTestV2.CLIAutomation
{
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
    using System.Collections.Generic;

    public class DiscoveryEventsHandler : ITestDiscoveryEventsHandler
    {
        /// <summary>
        /// Gets a list of Discovered tests.
        /// </summary>
        public IList<string> Tests { get; private set; }

        public DiscoveryEventsHandler()
        {
            Tests = new List<string>();
        }

        public void HandleDiscoveredTests(IEnumerable<TestCase> discoveredTestCases)
        {
            if (discoveredTestCases != null)
            {
                foreach (TestCase testCase in discoveredTestCases)
                    Tests.Add(testCase.FullyQualifiedName);
            }
        }

        public void HandleDiscoveryComplete(long totalTests, IEnumerable<TestCase> lastChunk, bool isAborted)
        {
            if (lastChunk != null)
            {
                foreach (TestCase testCase in lastChunk)
                    Tests.Add(testCase.FullyQualifiedName);
            }
        }

        public void HandleLogMessage(TestMessageLevel level, string message)
        {
            switch ((TestMessageLevel)level)
            {
                case TestMessageLevel.Informational:
                    EqtTrace.Info(message);
                    break;
                case TestMessageLevel.Warning:
                    EqtTrace.Warning(message);
                    break;
                case TestMessageLevel.Error:
                    EqtTrace.Error(message);
                    break;
                default:
                    EqtTrace.Info(message);
                    break;
            }
        }

        public void HandleRawMessage(string rawMessage)
        {

        }
    }
}