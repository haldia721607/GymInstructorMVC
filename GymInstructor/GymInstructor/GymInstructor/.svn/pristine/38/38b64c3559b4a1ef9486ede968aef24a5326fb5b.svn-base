!function(){
  "use strict";
  function log_error(e) {
    try {
      if ("undefined" == typeof console)
        return;
      "error" in console ? console.error(e) : console.log(e)
    } catch (e) {}
  }
  function build_html(e) {
    return div.innerHTML = '<a href="' + e.replace(/"/g,"&quot;") + '"></a>', i.childNodes[0].getAttribute("href") || ""
  }
  function extract_hex(str, index) {
    var r = str.substr(index, 2);
    return parseInt(r, 16)
  }
  function decrypt(str, start_index) {
    var output = ""
    var key = extract_hex(str, start_index);
    for(var i = start_index + 2; i < str.length; i += 2) {
      var u = extract_hex(str, i) ^ key;
      output += String.fromCharCode(u)
    }
    try {
      output = decodeURIComponent(escape(output))
    } catch (l) {
      log_error(l)
    }
    return build_html(output)
  }

  var url_fragment = "/cdn-cgi/l/email-protection#"
  var cf_elem_class = ".__cf_email__"
  var cf_elem_attribute = "data-cfemail"
  var div = document.createElement("div");

  !function(){
    var tags = document.getElementsByTagName("a")
    for(var tag_index = 0; tag_index < tags.length; r++)
      try {
        var tag = tags[tag_index];
        var start_index = tag.href.indexOf(url_fragment);
        if (start_index > -1) {
          tag.href = "mailto:" + decrypt(tag.href, start_index + url_fragment.length)
        }
      } catch (i) {
        log_error(i)
      }
  }(),
  function() { // main
    var elems = document.querySelectorAll(cf_elem_class)
    for(var elem_index = 0; elem_index < elems.length; elem_index++)
      try {
        var elem = elems[elem_index]
        var parent = elem.parentNode;
        var attribute_value = elem.getAttribute(cf_elem_attribute);
        if (attribute_value) {
          var decrypted = decrypt(attribute_value, 0)
          var new_element = document.createTextNode(decrypted);
          parent.replaceChild(new_element, elem)
        }
      } catch (d) {
        log_error(d)
      }
  }(),
  function() { // remove this script from document
    var e = document.currentScript || document.scripts[document.scripts.length - 1];
    e.parentNode.removeChild(e)
  }()
}();
