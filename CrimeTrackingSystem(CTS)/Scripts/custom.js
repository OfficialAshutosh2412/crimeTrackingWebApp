//navbar toggler
document.querySelector('#login-tog').addEventListener("click", () => {
    document.querySelector('.signin').classList.toggle('show-login');
});
//tools toggler
document.querySelector('#tool-toggler').addEventListener("click", () => {
    document.querySelector('.top-bar').classList.toggle('hide-top');
});
//preloader
var load=document.getElementById("preloader");
function myfunction() {
    load.style.display="none"
};
//gallery revealer
document.querySelector('#hide').addEventListener("click", () => {
    document.querySelector('.ind-hide').classList.toggle('ind-show');
});
//closebtn
document.querySelector('#close').addEventListener("click", () => {
    document.querySelector('.ind-hide').classList.toggle('ind-show');
});
//chandrayan gallery revealer
document.querySelector('#chandrahide').addEventListener("click", () => {
    document.querySelector('.chandra-hide').classList.toggle('chandra-show');
});
//closebtn
document.querySelector('#chandraclose').addEventListener("click", () => {
    document.querySelector('.chandra-hide').classList.toggle('chandra-show');
});
//feedback
function showFeed() {
    document.querySelector('.feedback').classList.toggle('show-feeds');
}
//theme panel hidden
function showTheme() {
    document.querySelector('.themes').classList.toggle('theme-hide');
    //auto hide theme panel
   setTimeout(() => {
       document.querySelector('.themes').classList.toggle('theme-hide');
    }, 2500);
}
//show setting
function showSetting() {
    document.querySelector('.themeSwitch').classList.toggle('showtheme');
    document.querySelector('.translator').classList.toggle('showtranslate');
    document.querySelector('.feedbackBtn').classList.toggle('showfeed');
    document.querySelector('.whats').classList.toggle('showwhats');
}


