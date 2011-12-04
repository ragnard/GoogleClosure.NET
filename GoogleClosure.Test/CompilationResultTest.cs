using Xunit;

namespace GoogleClosure.Test
{
    public class CompilationResultTest
    {
        #region Data

        public string SampleOutput =
            @"C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning : C:\Code\temp\QNET\Qbranch.QNET.Web\Resources\Scripts\qnet\ui\feedback.js:24: WARNING - dangerous use of the global this object
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :         this.xhrManager = new goog.net.XhrManager(0, {
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :         ^
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning : C:\Code\temp\QNET\Qbranch.QNET.Web\Resources\Scripts\qnet\ui\feedback.js:24: WARNING - dangerous use of this in static method qnet.ui.feedback.initialise
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :         this.xhrManager = new goog.net.XhrManager(0, {
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :         ^
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning : C:\Code\temp\QNET\Qbranch.QNET.Web\Resources\Scripts\qnet\ui\feedback.js:31: WARNING - dangerous use of the global this object
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :         this.xhrManager.setTimeoutInterval(10000);
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :         ^
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning : C:\Code\temp\QNET\Qbranch.QNET.Web\Resources\Scripts\qnet\ui\feedback.js:31: WARNING - dangerous use of this in static method qnet.ui.feedback.initialise
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :         this.xhrManager.setTimeoutInterval(10000);
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :         ^
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning : C:\Code\temp\QNET\Qbranch.QNET.Web\Resources\Scripts\qnet\ui\feedback.js:33: WARNING - dangerous use of the global this object
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :         this.orderService = new qnet.api.OrderService(this.xhrManager);
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :         ^
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning : C:\Code\temp\QNET\Qbranch.QNET.Web\Resources\Scripts\qnet\ui\feedback.js:33: WARNING - dangerous use of this in static method qnet.ui.feedback.initialise
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :         this.orderService = new qnet.api.OrderService(this.xhrManager);
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :         ^
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning : C:\Code\temp\QNET\Qbranch.QNET.Web\Resources\Scripts\qnet\ui\feedback.js:33: WARNING - dangerous use of the global this object
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :         this.orderService = new qnet.api.OrderService(this.xhrManager);
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :                                                       ^
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning : C:\Code\temp\QNET\Qbranch.QNET.Web\Resources\Scripts\qnet\ui\feedback.js:33: WARNING - dangerous use of this in static method qnet.ui.feedback.initialise
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :         this.orderService = new qnet.api.OrderService(this.xhrManager);
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :                                                       ^
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning : C:\Code\temp\QNET\Qbranch.QNET.Web\Resources\Scripts\qnet\ui\feedback.js:35: WARNING - dangerous use of the global this object
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :         this.controller = new qnet.ui.feedback.controller(this.orderService);
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :         ^
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning : C:\Code\temp\QNET\Qbranch.QNET.Web\Resources\Scripts\qnet\ui\feedback.js:35: WARNING - dangerous use of this in static method qnet.ui.feedback.initialise
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :         this.controller = new qnet.ui.feedback.controller(this.orderService);
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :         ^
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning : C:\Code\temp\QNET\Qbranch.QNET.Web\Resources\Scripts\qnet\ui\feedback.js:35: WARNING - dangerous use of the global this object
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :         this.controller = new qnet.ui.feedback.controller(this.orderService);
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :                                                           ^
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning : C:\Code\temp\QNET\Qbranch.QNET.Web\Resources\Scripts\qnet\ui\feedback.js:35: WARNING - dangerous use of this in static method qnet.ui.feedback.initialise
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :         this.controller = new qnet.ui.feedback.controller(this.orderService);
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning :                                                           ^
C:\Code\temp\QNET\Qbranch.QNET.Web\GoogleClosure.targets(72,5): warning : 0 error(s), 12 warning(s)";

    #endregion

        [Fact]
        public void CanParseErrorsAndWarningsFromCompilationResult()
        {
            var result = CompilationResult.CreateFrom(SampleOutput, true);



        }
    }
}