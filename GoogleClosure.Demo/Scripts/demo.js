var h=void 0,j=!0,k=null,m=!1,p,q=this;function r(){}
function s(a){var b=typeof a;if("object"==b)if(a){if(a instanceof Array)return"array";if(a instanceof Object)return b;var c=Object.prototype.toString.call(a);if("[object Window]"==c)return"object";if("[object Array]"==c||"number"==typeof a.length&&"undefined"!=typeof a.splice&&"undefined"!=typeof a.propertyIsEnumerable&&!a.propertyIsEnumerable("splice"))return"array";if("[object Function]"==c||"undefined"!=typeof a.call&&"undefined"!=typeof a.propertyIsEnumerable&&!a.propertyIsEnumerable("call"))return"function"}else return"null";
else if("function"==b&&"undefined"==typeof a.call)return"object";return b}function u(a){return"array"==s(a)}function v(a){var b=s(a);return"array"==b||"object"==b&&"number"==typeof a.length}function w(a){return"string"==typeof a}function aa(a){return"function"==s(a)}function ba(a){a=s(a);return"object"==a||"array"==a||"function"==a}var x="closure_uid_"+Math.floor(2147483648*Math.random()).toString(36),ca=0;function da(a,b,c){return a.call.apply(a.bind,arguments)}
function ea(a,b,c){if(!a)throw Error();if(2<arguments.length){var d=Array.prototype.slice.call(arguments,2);return function(){var c=Array.prototype.slice.call(arguments);Array.prototype.unshift.apply(c,d);return a.apply(b,c)}}return function(){return a.apply(b,arguments)}}function fa(a,b,c){fa=Function.prototype.bind&&-1!=Function.prototype.bind.toString().indexOf("native code")?da:ea;return fa.apply(k,arguments)}var ga=Date.now||function(){return+new Date};
function ha(a,b){function c(){}c.prototype=b.prototype;a.L=b.prototype;a.prototype=new c};var ia=/^[a-zA-Z0-9\-_.!~*'()]*$/;function ja(a){a=""+a;return!ia.test(a)?encodeURIComponent(a):a}function ka(a){if(!la.test(a))return a;-1!=a.indexOf("&")&&(a=a.replace(ma,"&amp;"));-1!=a.indexOf("<")&&(a=a.replace(na,"&lt;"));-1!=a.indexOf(">")&&(a=a.replace(oa,"&gt;"));-1!=a.indexOf('"')&&(a=a.replace(pa,"&quot;"));return a}var ma=/&/g,na=/</g,oa=/>/g,pa=/\"/g,la=/[&<>\"]/;
function qa(a,b){for(var c=0,d=(""+a).replace(/^[\s\xa0]+|[\s\xa0]+$/g,"").split("."),f=(""+b).replace(/^[\s\xa0]+|[\s\xa0]+$/g,"").split("."),e=Math.max(d.length,f.length),g=0;0==c&&g<e;g++){var i=d[g]||"",l=f[g]||"",n=RegExp("(\\d*)(\\D*)","g"),y=RegExp("(\\d*)(\\D*)","g");do{var o=n.exec(i)||["","",""],t=y.exec(l)||["","",""];if(0==o[0].length&&0==t[0].length)break;c=((0==o[1].length?0:parseInt(o[1],10))<(0==t[1].length?0:parseInt(t[1],10))?-1:(0==o[1].length?0:parseInt(o[1],10))>(0==t[1].length?
0:parseInt(t[1],10))?1:0)||((0==o[2].length)<(0==t[2].length)?-1:(0==o[2].length)>(0==t[2].length)?1:0)||(o[2]<t[2]?-1:o[2]>t[2]?1:0)}while(0==c)}return c};var A=Array.prototype,ra=A.indexOf?function(a,b,c){return A.indexOf.call(a,b,c)}:function(a,b,c){c=c==k?0:0>c?Math.max(0,a.length+c):c;if(w(a))return!w(b)||1!=b.length?-1:a.indexOf(b,c);for(;c<a.length;c++)if(c in a&&a[c]===b)return c;return-1},sa=A.forEach?function(a,b,c){A.forEach.call(a,b,c)}:function(a,b,c){for(var d=a.length,f=w(a)?a.split(""):a,e=0;e<d;e++)e in f&&b.call(c,f[e],e,a)},ta=A.map?function(a,b,c){return A.map.call(a,b,c)}:function(a,b,c){for(var d=a.length,f=Array(d),e=w(a)?a.split(""):
a,g=0;g<d;g++)g in e&&(f[g]=b.call(c,e[g],g,a));return f};function ua(a){return A.concat.apply(A,arguments)}function va(a){if(u(a))return ua(a);for(var b=[],c=0,d=a.length;c<d;c++)b[c]=a[c];return b}function wa(a,b){for(var c=1;c<arguments.length;c++){var d=arguments[c],f;if(u(d)||(f=v(d))&&d.hasOwnProperty("callee"))a.push.apply(a,d);else if(f)for(var e=a.length,g=d.length,i=0;i<g;i++)a[e+i]=d[i];else a.push(d)}}
function xa(a,b,c){return 2>=arguments.length?A.slice.call(a,b):A.slice.call(a,b,c)};var B,ya,za,Aa;function Ba(){return q.navigator?q.navigator.userAgent:k}Aa=za=ya=B=m;var Ca;if(Ca=Ba()){var Da=q.navigator;B=0==Ca.indexOf("Opera");ya=!B&&-1!=Ca.indexOf("MSIE");za=!B&&-1!=Ca.indexOf("WebKit");Aa=!B&&!za&&"Gecko"==Da.product}var C=ya,Ea=Aa,Fa=za,Ga;
a:{var Ha="",D;if(B&&q.opera)var Ia=q.opera.version,Ha="function"==typeof Ia?Ia():Ia;else if(Ea?D=/rv\:([^\);]+)(\)|;)/:C?D=/MSIE\s+([^\);]+)(\)|;)/:Fa&&(D=/WebKit\/(\S+)/),D)var Ja=D.exec(Ba()),Ha=Ja?Ja[1]:"";if(C){var Ka,La=q.document;Ka=La?La.documentMode:h;if(Ka>parseFloat(Ha)){Ga=""+Ka;break a}}Ga=Ha}var Ma={};function Na(a){return Ma[a]||(Ma[a]=0<=qa(Ga,a))}var Oa={};function Pa(){return Oa[9]||(Oa[9]=C&&document.documentMode&&9<=document.documentMode)};var Qa=!C||Pa();!Ea&&!C||C&&Pa()||Ea&&Na("1.9.1");C&&Na("9");function Ra(a,b){var c;c=(c=a.className)&&"function"==typeof c.split?c.split(/\s+/):[];var d=xa(arguments,1),f;f=c;for(var e=0,g=0;g<d.length;g++)0<=ra(f,d[g])||(f.push(d[g]),e++);f=e==d.length;a.className=c.join(" ");return f};function Sa(a,b){for(var c in a)b.call(h,a[c],c,a)}function Ta(a){var b=[],c=0,d;for(d in a)b[c++]=a[d];return b}function Ua(a){var b=[],c=0,d;for(d in a)b[c++]=d;return b}var Va="constructor hasOwnProperty isPrototypeOf propertyIsEnumerable toLocaleString toString valueOf".split(" ");function Wa(a,b){for(var c,d,f=1;f<arguments.length;f++){d=arguments[f];for(c in d)a[c]=d[c];for(var e=0;e<Va.length;e++)c=Va[e],Object.prototype.hasOwnProperty.call(d,c)&&(a[c]=d[c])}};function Xa(a,b){Sa(b,function(b,d){"style"==d?a.style.cssText=b:"class"==d?a.className=b:"for"==d?a.htmlFor=b:d in Ya?a.setAttribute(Ya[d],b):0==d.lastIndexOf("aria-",0)?a.setAttribute(d,b):a[d]=b})}var Ya={cellpadding:"cellPadding",cellspacing:"cellSpacing",colspan:"colSpan",rowspan:"rowSpan",valign:"vAlign",height:"height",width:"width",usemap:"useMap",frameborder:"frameBorder",maxlength:"maxLength",type:"type"};
function E(a,b,c){var d=arguments,f=document,e=d[0],g=d[1];if(!Qa&&g&&(g.name||g.type)){e=["<",e];g.name&&e.push(' name="',ka(g.name),'"');if(g.type){e.push(' type="',ka(g.type),'"');var i={};Wa(i,g);g=i;delete g.type}e.push(">");e=e.join("")}e=f.createElement(e);g&&(w(g)?e.className=g:u(g)?Ra.apply(k,[e].concat(g)):Xa(e,g));2<d.length&&Za(f,e,d);return e}
function Za(a,b,c){function d(c){c&&b.appendChild(w(c)?a.createTextNode(c):c)}for(var f=2;f<c.length;f++){var e=c[f];v(e)&&!(ba(e)&&0<e.nodeType)?sa($a(e)?va(e):e,d):d(e)}}function ab(a){for(var b;b=a.firstChild;)a.removeChild(b)}function $a(a){if(a&&"number"==typeof a.length){if(ba(a))return"function"==typeof a.item||"string"==typeof a.item;if(aa(a))return"function"==typeof a.item}return m};function bb(a){if("function"==typeof a.o)return a.o();if(w(a))return a.split("");if(v(a)){for(var b=[],c=a.length,d=0;d<c;d++)b.push(a[d]);return b}return Ta(a)}function cb(a,b,c){if("function"==typeof a.forEach)a.forEach(b,c);else if(v(a)||w(a))sa(a,b,c);else{var d;if("function"==typeof a.t)d=a.t();else if("function"!=typeof a.o)if(v(a)||w(a)){d=[];for(var f=a.length,e=0;e<f;e++)d.push(e)}else d=Ua(a);else d=h;for(var f=bb(a),e=f.length,g=0;g<e;g++)b.call(c,f[g],d&&d[g],a)}};function db(a,b){this.i={};this.c=[];var c=arguments.length;if(1<c){if(c%2)throw Error("Uneven number of arguments");for(var d=0;d<c;d+=2)this.set(arguments[d],arguments[d+1])}else if(a){a instanceof db?(c=a.t(),d=a.o()):(c=Ua(a),d=Ta(a));for(var f=0;f<c.length;f++)this.set(c[f],d[f])}}p=db.prototype;p.a=0;p.o=function(){eb(this);for(var a=[],b=0;b<this.c.length;b++)a.push(this.i[this.c[b]]);return a};p.t=function(){eb(this);return this.c.concat()};p.j=function(a){return F(this.i,a)};
p.remove=function(a){return F(this.i,a)?(delete this.i[a],this.a--,this.c.length>2*this.a&&eb(this),j):m};function eb(a){if(a.a!=a.c.length){for(var b=0,c=0;b<a.c.length;){var d=a.c[b];F(a.i,d)&&(a.c[c++]=d);b++}a.c.length=c}if(a.a!=a.c.length){for(var f={},c=b=0;b<a.c.length;)d=a.c[b],F(f,d)||(a.c[c++]=d,f[d]=1),b++;a.c.length=c}}p.get=function(a,b){return F(this.i,a)?this.i[a]:b};p.set=function(a,b){F(this.i,a)||(this.a++,this.c.push(a));this.i[a]=b};p.q=function(){return new db(this)};
function F(a,b){return Object.prototype.hasOwnProperty.call(a,b)};function fb(a){var b=a.type;if(b===h)return k;switch(b.toLowerCase()){case "checkbox":case "radio":return a.checked?a.value:k;case "select-one":return b=a.selectedIndex,0<=b?a.options[b].value:k;case "select-multiple":for(var b=[],c,d=0;c=a.options[d];d++)c.selected&&b.push(c.value);return b.length?b:k;default:return a.value!==h?a.value:k}};var gb;!C||Pa();var hb=C&&!Na("8");function G(){}G.prototype.Q=m;G.prototype.r=function(){this.Q||(this.Q=j,this.s())};G.prototype.s=function(){this.aa&&ib.apply(k,this.aa)};function ib(a){for(var b=0,c=arguments.length;b<c;++b){var d=arguments[b];v(d)?ib.apply(k,d):d&&"function"==typeof d.r&&d.r()}};function H(a,b){this.type=a;this.currentTarget=this.target=b}ha(H,G);H.prototype.s=function(){delete this.type;delete this.target;delete this.currentTarget};H.prototype.K=m;H.prototype.W=j;H.prototype.preventDefault=function(){this.W=m};function jb(a){jb[" "](a);return a}jb[" "]=r;function I(a,b){a&&this.D(a,b)}ha(I,H);p=I.prototype;p.target=k;p.relatedTarget=k;p.offsetX=0;p.offsetY=0;p.clientX=0;p.clientY=0;p.screenX=0;p.screenY=0;p.button=0;p.keyCode=0;p.charCode=0;p.ctrlKey=m;p.altKey=m;p.shiftKey=m;p.metaKey=m;p.J=k;
p.D=function(a,b){var c=this.type=a.type;H.call(this,c);this.target=a.target||a.srcElement;this.currentTarget=b;var d=a.relatedTarget;if(d){if(Ea){var f;a:{try{jb(d.nodeName);f=j;break a}catch(e){}f=m}f||(d=k)}}else"mouseover"==c?d=a.fromElement:"mouseout"==c&&(d=a.toElement);this.relatedTarget=d;this.offsetX=a.offsetX!==h?a.offsetX:a.layerX;this.offsetY=a.offsetY!==h?a.offsetY:a.layerY;this.clientX=a.clientX!==h?a.clientX:a.pageX;this.clientY=a.clientY!==h?a.clientY:a.pageY;this.screenX=a.screenX||
0;this.screenY=a.screenY||0;this.button=a.button;this.keyCode=a.keyCode||0;this.charCode=a.charCode||("keypress"==c?a.keyCode:0);this.ctrlKey=a.ctrlKey;this.altKey=a.altKey;this.shiftKey=a.shiftKey;this.metaKey=a.metaKey;this.state=a.state;this.J=a;delete this.W;delete this.K};p.preventDefault=function(){I.L.preventDefault.call(this);var a=this.J;if(a.preventDefault)a.preventDefault();else if(a.returnValue=m,hb)try{if(a.ctrlKey||112<=a.keyCode&&123>=a.keyCode)a.keyCode=-1}catch(b){}};
p.s=function(){I.L.s.call(this);this.relatedTarget=this.currentTarget=this.target=this.J=k};function kb(){}var lb=0;p=kb.prototype;p.key=0;p.w=m;p.N=m;p.D=function(a,b,c,d,f,e){if(aa(a))this.S=j;else if(a&&a.handleEvent&&aa(a.handleEvent))this.S=m;else throw Error("Invalid listener argument");this.F=a;this.V=b;this.src=c;this.type=d;this.capture=!!f;this.R=e;this.N=m;this.key=++lb;this.w=m};p.handleEvent=function(a){return this.S?this.F.call(this.R||this.src,a):this.F.handleEvent.call(this.F,a)};function J(a,b){this.T=b;this.n=[];if(a>this.T)throw Error("[goog.structs.SimplePool] Initial cannot be greater than max");for(var c=0;c<a;c++)this.n.push(this.g?this.g():{})}ha(J,G);J.prototype.g=k;J.prototype.P=k;function K(a){return a.n.length?a.n.pop():a.g?a.g():{}}function L(a,b){a.n.length<a.T?a.n.push(b):mb(a,b)}function mb(a,b){if(a.P)a.P(b);else if(ba(b))if(aa(b.r))b.r();else for(var c in b)delete b[c]}
J.prototype.s=function(){J.L.s.call(this);for(var a=this.n;a.length;)mb(this,a.pop());delete this.n};var nb,ob=(nb="ScriptEngine"in q&&"JScript"==q.ScriptEngine())?q.ScriptEngineMajorVersion()+"."+q.ScriptEngineMinorVersion()+"."+q.ScriptEngineBuildVersion():"0";var pb,qb,M,rb,sb,tb,ub,vb,wb,xb,yb;
(function(){function a(){return{a:0,v:0}}function b(){return[]}function c(){function a(b){b=g.call(a.src,a.key,b);if(!b)return b}return a}function d(){return new kb}function f(){return new I}var e=nb&&!(0<=qa(ob,"5.7")),g;tb=function(a){g=a};if(e){pb=function(){return K(i)};qb=function(a){L(i,a)};M=function(){return K(l)};rb=function(a){L(l,a)};sb=function(){return K(n)};ub=function(){L(n,c())};vb=function(){return K(y)};wb=function(a){L(y,a)};xb=function(){return K(o)};yb=function(a){L(o,a)};var i=
new J(0,600);i.g=a;var l=new J(0,600);l.g=b;var n=new J(0,600);n.g=c;var y=new J(0,600);y.g=d;var o=new J(0,600);o.g=f}else pb=a,qb=r,M=b,rb=r,sb=c,ub=r,vb=d,wb=r,xb=f,yb=r})();var N={},P={},Q={},R={};
function zb(a,b,c,d,f){if(b)if(u(b))for(var e=0;e<b.length;e++)zb(a,b[e],c,d,f);else{var d=!!d,g=P;b in g||(g[b]=pb());g=g[b];d in g||(g[d]=pb(),g.a++);var g=g[d],i=a[x]||(a[x]=++ca),l;g.v++;if(g[i]){l=g[i];for(e=0;e<l.length;e++)if(g=l[e],g.F==c&&g.R==f){if(g.w)break;return}}else l=g[i]=M(),g.a++;e=sb();e.src=a;g=vb();g.D(c,e,a,b,d,f);c=g.key;e.key=c;l.push(g);N[c]=g;Q[i]||(Q[i]=M());Q[i].push(g);a.addEventListener?(a==q||!a.$)&&a.addEventListener(b,e,d):a.attachEvent(b in R?R[b]:R[b]="on"+b,e)}else throw Error("Invalid event type");
}function Ab(a,b,c,d){if(!d.G&&d.U){for(var f=0,e=0;f<d.length;f++)if(d[f].w){var g=d[f].V;g.src=k;ub(g);wb(d[f])}else f!=e&&(d[e]=d[f]),e++;d.length=e;d.U=m;if(0==e&&(rb(d),delete P[a][b][c],P[a][b].a--,0==P[a][b].a&&(qb(P[a][b]),delete P[a][b],P[a].a--),0==P[a].a))qb(P[a]),delete P[a]}}function Bb(a,b,c,d,f){var e=1,b=b[x]||(b[x]=++ca);if(a[b]){a.v--;a=a[b];a.G?a.G++:a.G=1;try{for(var g=a.length,i=0;i<g;i++){var l=a[i];l&&!l.w&&(e&=Cb(l,f)!==m)}}finally{a.G--,Ab(c,d,b,a)}}return Boolean(e)}
function Cb(a,b){var c=a.handleEvent(b);if(a.N){var d=a.key;if(N[d]){var f=N[d];if(!f.w){var e=f.src,g=f.type,i=f.V,l=f.capture;e.removeEventListener?(e==q||!e.$)&&e.removeEventListener(g,i,l):e.detachEvent&&e.detachEvent(g in R?R[g]:R[g]="on"+g,i);e=e[x]||(e[x]=++ca);i=P[g][l][e];if(Q[e]){var n=Q[e],y=ra(n,f);0<=y&&A.splice.call(n,y,1);0==n.length&&delete Q[e]}f.w=j;i.U=j;Ab(g,l,e,i);delete N[d]}}}return c}
tb(function(a,b){if(!N[a])return j;var c=N[a],d=c.type,f=P;if(!(d in f))return j;var f=f[d],e,g;gb===h&&(gb=C&&!q.addEventListener);if(gb){var i;if(!(i=b))a:{i=["window","event"];for(var l=q;e=i.shift();)if(l[e]!=k)l=l[e];else{i=k;break a}i=l}e=i;i=j in f;l=m in f;if(i){if(0>e.keyCode||e.returnValue!=h)return j;a:{var n=m;if(0==e.keyCode)try{e.keyCode=-1;break a}catch(y){n=j}if(n||e.returnValue==h)e.returnValue=j}}n=xb();n.D(e,this);e=j;try{if(i){for(var o=M(),t=n.currentTarget;t;t=t.parentNode)o.push(t);
g=f[j];g.v=g.a;for(var z=o.length-1;!n.K&&0<=z&&g.v;z--)n.currentTarget=o[z],e&=Bb(g,o[z],d,j,n);if(l){g=f[m];g.v=g.a;for(z=0;!n.K&&z<o.length&&g.v;z++)n.currentTarget=o[z],e&=Bb(g,o[z],d,m,n)}}else e=Cb(c,n)}finally{o&&(o.length=0,rb(o)),n.r(),yb(n)}return e}d=new I(b,this);try{e=Cb(c,d)}finally{d.r()}return e});function Db(a){this.O=a;this.X=w("searchForm")?document.getElementById("searchForm"):"searchForm";this.ga=w("tagsInput")?document.getElementById("tagsInput"):"tagsInput";this.H=w("resultDiv")?document.getElementById("resultDiv"):"resultDiv";zb(this.X,"submit",this.fa,m,this)}Db.prototype.fa=function(a){a.preventDefault();Eb(this.O)};Db.prototype.update=function(a){a=ta(a.items,this.Z);ab(this.H);sa(a,function(a){this.H.appendChild(a)},this)};
Db.prototype.Z=function(a){return E("div",{"class":"item"},E("a",{href:a.link},E("h3",{"class":"itemTitle"},a.title)),E("p",{"class":"itemAuthor"},a.author),E("a",{href:a.link},E("img",{"class":"itemImage",src:a.media.m})),E("hr"))};var Fb=RegExp("^(?:([^:/?#.]+):)?(?://(?:([^/?#]*)@)?([\\w\\d\\-\\u0100-\\uffff.%]*)(?::([0-9]+))?)?([^?#]+)?(?:\\?([^#]*))?(?:#(.*))?$");function Gb(a,b){var c;a instanceof Gb?(this.z(b==k?a.e:b),Hb(this,a.k),Ib(this,a.C),Jb(this,a.l),Kb(this,a.u),Lb(this,a.p),Mb(this,a.f.q()),Nb(this,a.A)):a&&(c=(""+a).match(Fb))?(this.z(!!b),Hb(this,c[1]||"",j),Ib(this,c[2]||"",j),Jb(this,c[3]||"",j),Kb(this,c[4]),Lb(this,c[5]||"",j),Mb(this,c[6]||"",j),Nb(this,c[7]||"",j)):(this.z(!!b),this.f=new S(k,this,this.e))}p=Gb.prototype;p.k="";p.C="";p.l="";p.u=k;p.p="";p.A="";p.ba=m;p.e=m;
p.toString=function(){if(this.d)return this.d;var a=[];this.k&&a.push(T(this.k,Ob),":");this.l&&(a.push("//"),this.C&&a.push(T(this.C,Ob),"@"),a.push(w(this.l)?encodeURIComponent(this.l):k),this.u!=k&&a.push(":",""+this.u));this.p&&(this.l&&"/"!=this.p.charAt(0)&&a.push("/"),a.push(T(this.p,"/"==this.p.charAt(0)?Pb:Qb)));var b=""+this.f;b&&a.push("?",b);this.A&&a.push("#",T(this.A,Rb));return this.d=a.join("")};
p.q=function(){var a=this.k,b=this.C,c=this.l,d=this.u,f=this.p,e=this.f.q(),g=this.A,i=new Gb(k,this.e);a&&Hb(i,a);b&&Ib(i,b);c&&Jb(i,c);d&&Kb(i,d);f&&Lb(i,f);e&&Mb(i,e);g&&Nb(i,g);return i};function Hb(a,b,c){U(a);delete a.d;a.k=c?b?decodeURIComponent(b):"":b;a.k&&(a.k=a.k.replace(/:$/,""))}function Ib(a,b,c){U(a);delete a.d;a.C=c?b?decodeURIComponent(b):"":b}function Jb(a,b,c){U(a);delete a.d;a.l=c?b?decodeURIComponent(b):"":b}
function Kb(a,b){U(a);delete a.d;if(b){b=Number(b);if(isNaN(b)||0>b)throw Error("Bad port number "+b);a.u=b}else a.u=k}function Lb(a,b,c){U(a);delete a.d;a.p=c?b?decodeURIComponent(b):"":b}function Mb(a,b,c){U(a);delete a.d;b instanceof S?(a.f=b,a.f.B=a,a.f.z(a.e)):(c||(b=T(b,Sb)),a.f=new S(b,a,a.e))}function Tb(a,b,c){U(a);delete a.d;u(c)||(c=[""+c]);a=a.f;V(a);W(a);b=X(a,b);if(a.j(b)){var d=a.b.get(b);u(d)?a.a-=d.length:a.a--}0<c.length&&(a.b.set(b,c),a.a+=c.length)}
function Nb(a,b,c){U(a);delete a.d;a.A=c?b?decodeURIComponent(b):"":b}function U(a){if(a.ba)throw Error("Tried to modify a read-only Uri");}p.z=function(a){this.e=a;this.f&&this.f.z(a);return this};var Ub=/^[a-zA-Z0-9\-_.!~*'():\/;?]*$/;function T(a,b){var c=k;w(a)&&(c=a,Ub.test(c)||(c=encodeURI(a)),0<=c.search(b)&&(c=c.replace(b,Vb)));return c}function Vb(a){a=a.charCodeAt(0);return"%"+(a>>4&15).toString(16)+(a&15).toString(16)}var Ob=/[#\/\?@]/g,Qb=/[\#\?:]/g,Pb=/[\#\?]/g,Sb=/[\#\?@]/g,Rb=/#/g;
function S(a,b,c){this.h=a||k;this.B=b||k;this.e=!!c}function V(a){if(!a.b&&(a.b=new db,a.a=0,a.h))for(var b=a.h.split("&"),c=0;c<b.length;c++){var d=b[c].indexOf("="),f=k,e=k;0<=d?(f=b[c].substring(0,d),e=b[c].substring(d+1)):f=b[c];f=decodeURIComponent(f.replace(/\+/g," "));f=X(a,f);a.add(f,e?decodeURIComponent(e.replace(/\+/g," ")):"")}}p=S.prototype;p.b=k;p.a=k;
p.add=function(a,b){V(this);W(this);a=X(this,a);if(this.j(a)){var c=this.b.get(a);u(c)?c.push(b):this.b.set(a,[c,b])}else this.b.set(a,b);this.a++;return this};p.remove=function(a){V(this);a=X(this,a);if(this.b.j(a)){W(this);var b=this.b.get(a);u(b)?this.a-=b.length:this.a--;return this.b.remove(a)}return m};p.j=function(a){V(this);a=X(this,a);return this.b.j(a)};
p.t=function(){V(this);for(var a=this.b.o(),b=this.b.t(),c=[],d=0;d<b.length;d++){var f=a[d];if(u(f))for(var e=0;e<f.length;e++)c.push(b[d]);else c.push(b[d])}return c};p.o=function(a){V(this);if(a)if(a=X(this,a),this.j(a)){var b=this.b.get(a);if(u(b))return b;a=[];a.push(b)}else a=[];else for(var b=this.b.o(),a=[],c=0;c<b.length;c++){var d=b[c];u(d)?wa(a,d):a.push(d)}return a};
p.set=function(a,b){V(this);W(this);a=X(this,a);if(this.j(a)){var c=this.b.get(a);u(c)?this.a-=c.length:this.a--}this.b.set(a,b);this.a++;return this};p.get=function(a,b){V(this);a=X(this,a);if(this.j(a)){var c=this.b.get(a);return u(c)?c[0]:c}return b};
p.toString=function(){if(this.h)return this.h;if(!this.b)return"";for(var a=[],b=0,c=this.b.t(),d=0;d<c.length;d++){var f=c[d],e=ja(f),f=this.b.get(f);if(u(f))for(var g=0;g<f.length;g++)0<b&&a.push("&"),a.push(e),""!==f[g]&&a.push("=",ja(f[g])),b++;else 0<b&&a.push("&"),a.push(e),""!==f&&a.push("=",ja(f)),b++}return this.h=a.join("")};function W(a){delete a.I;delete a.h;a.B&&delete a.B.d}p.q=function(){var a=new S;this.I&&(a.I=this.I);this.h&&(a.h=this.h);this.b&&(a.b=this.b.q());return a};
function X(a,b){var c=""+b;a.e&&(c=c.toLowerCase());return c}p.z=function(a){a&&!this.e&&(V(this),W(this),cb(this.b,function(a,c){var d=c.toLowerCase();c!=d&&(this.remove(c),this.add(d,a))},this));this.e=a};function Wb(a,b){this.B=new Gb(a);this.Y=b?b:"callback";this.M=5E3}var Xb=0;
Wb.prototype.send=function(a,b,c,d){a=a||k;if(!document.documentElement.firstChild)return c&&c(a),k;d=d||"_"+(Xb++).toString(36)+ga().toString(36);q._callbacks_||(q._callbacks_={});var f;f=document.createElement("script");var e=k;0<this.M&&(e=q.setTimeout(Yb(d,f,a,c),this.M));c=this.B.q();if(a)for(var g in a)(!a.hasOwnProperty||a.hasOwnProperty(g))&&Tb(c,g,a[g]);b&&(q._callbacks_[d]=Zb(d,f,b,e),Tb(c,this.Y,"_callbacks_."+d));Xa(f,{type:"text/javascript",id:d,charset:"UTF-8",src:c.toString()});document.getElementsByTagName("head")[0].appendChild(f);
return{ha:d,M:e}};function Yb(a,b,c,d){return function(){$b(a,b,m);d&&d(c)}}function Zb(a,b,c,d){return function(f){q.clearTimeout(d);$b(a,b,j);c.apply(h,arguments)}}function $b(a,b,c){q.setTimeout(function(){b&&b.parentNode&&b.parentNode.removeChild(b)},0);q._callbacks_[a]&&(c?delete q._callbacks_[a]:q._callbacks_[a]=r)};function ac(){this.view=new Db(this);this.ca=new Wb(this.view.X.getAttribute("action"),"jsoncallback");Eb(this)}function Eb(a){var b={format:"json",tags:fb(a.view.ga).split(/\s+/).join(",")},c=a.view;ab(c.H);var c=c.H,d=E("p",k,"Searching public Flickr feed...");c.appendChild(d);a.ca.send(b,fa(a.ea,a),fa(a.da,a))}ac.prototype.ea=function(a){this.view.update(a)};ac.prototype.da=function(a){console.log("error",a)};function bc(){window.O=new ac}var Y=["demo","initialize"],Z=q;!(Y[0]in Z)&&Z.execScript&&Z.execScript("var "+Y[0]);for(var $;Y.length&&($=Y.shift());)!Y.length&&bc!==h?Z[$]=bc:Z=Z[$]?Z[$]:Z[$]={};
//@ sourceMappingURL=/Scripts/demo.js.map