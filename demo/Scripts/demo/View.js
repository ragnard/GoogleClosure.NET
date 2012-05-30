goog.provide('demo.View');

goog.require('goog.dom');
goog.require('goog.dom.forms');
goog.require('goog.events');
goog.require('goog.events.EventType');
goog.require('goog.string');


/**
* @constructor
*/
demo.View = function (controller) {
  this.controller = controller;
  this.searchForm = goog.dom.getElement('searchForm');
  this.tagsInput = goog.dom.getElement('tagsInput');
  this.resultDiv = goog.dom.getElement('resultDiv');

  this.registerEvents();
};

demo.View.prototype.registerEvents = function() {
  goog.events.listen(this.searchForm, goog.events.EventType.SUBMIT, this.onSearchFormSubmit, false, this);
};

demo.View.prototype.onSearchFormSubmit = function(ev) {
  ev.preventDefault();
  this.controller.performSearch();
};

demo.View.prototype.getSearchUrl = function() {
  return this.searchForm.getAttribute('action');
};

demo.View.prototype.getTags = function () {
  var value = goog.dom.forms.getValue(this.tagsInput);

  return value.split(/\s+/);
};

demo.View.prototype.showMessage = function(s) {
  goog.dom.removeChildren(this.resultDiv);

  goog.dom.appendChild(this.resultDiv, goog.dom.createDom('p', null, s));
};

demo.View.prototype.update = function (feed) {

  var itemViews = goog.array.map(feed['items'], this.createItemView);

  goog.dom.removeChildren(this.resultDiv);

  goog.array.forEach(itemViews, function (view) {
    goog.dom.appendChild(this.resultDiv, view);
  }, this);

};

demo.View.prototype.createItemView = function (item) {
  return goog.dom.createDom('div', { 'class': 'item' },
    goog.dom.createDom('a', { 'href': item['link']},
      goog.dom.createDom('h3', { 'class': 'itemTitle' }, item['title'])),
    goog.dom.createDom('p', { 'class': 'itemAuthor' }, item['author']),
    goog.dom.createDom('a', { 'href': item['link']},
      goog.dom.createDom('img', { 'class': 'itemImage', 'src': item['media']['m'] })),
    goog.dom.createDom('hr'));
}