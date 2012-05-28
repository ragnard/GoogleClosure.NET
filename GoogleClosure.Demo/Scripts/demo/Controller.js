goog.provide('demo.Controller');

goog.require('goog.events');
goog.require('goog.net.Jsonp');

goog.require('demo.View');

/**
* Simple Controller-ish class responsible for handling user interaction,
* communicating with the rest of the world (ie. the Flickr API) and 
* updating the view as required.
*
* @constructor
*/
demo.Controller = function() {
  this.view = new demo.View(this);
  this.jsonp = new goog.net.Jsonp(this.view.getSearchUrl(), 'jsoncallback');
  this.performSearch();
};

demo.Controller.prototype.performSearch = function() {
  
  var tags = this.view.getTags();

  var payload = {
    'format': 'json',
    'tags': tags.join(',')
  };

  this.view.showMessage('Searching public Flickr feed...');
  this.jsonp.send(payload, goog.bind(this.onReplyReceived, this), goog.bind(this.onError, this));
};

demo.Controller.prototype.onReplyReceived = function(response) {
  this.view.update(response);
};

demo.Controller.prototype.onError = function(payload) {
  console.log('error', payload);
};

