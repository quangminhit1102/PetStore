const expandableS = document.querySelectorAll(".expandable");
expandableS.forEach((e) => {
  e.querySelector(".expandable-text").onclick = (event) => {
    console.log("click");
    event.preventDefault();
    e.querySelector(".expandable-text").classList.toggle("active");
    e.querySelector(".expandable-text").classList.contains("active")
      ? e.querySelector(".treeview").classList.add("active")
      : e.querySelector(".treeview").classList.remove("active");
  };
});

const sortCookieName = "sort";
const sortList = "list";
const sortGrid = "grid";
const navLinks = document.querySelectorAll(".nav-link");
function setCookie(cname, cvalue, exdays) {
  var d = new Date();
  d.setTime(d.getTime() + exdays * 24 * 60 * 60 * 1000);
  var expires = "expires=" + d.toUTCString();
  document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
function getCookie(cname) {
  var name = cname + "=";
  var ca = document.cookie.split(";");
  for (var i = 0; i < ca.length; i++) {
    var c = ca[i];
    while (c.charAt(0) == " ") {
      c = c.substring(1);
    }
    if (c.indexOf(name) == 0) {
      return c.substring(name.length, c.length);
    }
  }
  return "";
}
loadSort();
function loadSort() {
  var sort = getCookie(sortCookieName);
  sort === "" ? sortGrid : sort;
  if (sort === "grid") {
    document.getElementById("pills-grid-tab").classList.add("active");
    document.getElementById("sort-grid").classList.add("show");
    document.getElementById("sort-grid").classList.add("active");
  } else {
    document.getElementById("pills-list-tab").classList.add("active");
    document.getElementById("sort-list").classList.add("show");
    document.getElementById("sort-list").classList.add("active");
  }
}
function switchSort() {
  setTimeout(()=>{
    if (document.getElementById("pills-grid-tab").classList.contains("active")) {
      console.log("grid")
      setCookie(sortCookieName, sortGrid);
    }
    if (document.getElementById("pills-list-tab").classList.contains("active")) {
      setCookie(sortCookieName, sortList);
      console.log("list")
    }
  } , 3000);
 
}
