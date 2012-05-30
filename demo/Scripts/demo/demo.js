goog.provide('demo');

goog.require('demo.Controller');

/*
* This function bootstraps the demo application by creating a new 
* instance of the demo.Controller class.
*/
demo.initialize = function() {
  window.controller = new demo.Controller();
};

/*
* We want to make sure demo.initialize is callable from the HTML page 
* after compilation and potential renaming. This can be accomplished
* by the goog.exportSymbol function.
*/
goog.exportSymbol('demo.initialize', demo.initialize);
