(function (window) {
    var items = [
        { id: 8, text: 'A类：人的行为' },
        { id: 9, text: 'B类：工具和工艺设备' },
        { id: 10, text: 'C类：人机工程' },
        { id: 11, text: 'D类：规章制度' },
        { id: 12, text: 'E类：作业环境和应急' }
    ];
    var SAFETYCHECK = {
        TYPES: {
            getTextById: function (id) {
                for (var index in items) {
                    if (items[index].id == id)
                        return items[index].text;
                }
                return null;
            }
        }
    };
    window.SAFETYCHECK = SAFETYCHECK;


    var Sys = {};
    var ua = navigator.userAgent.toLowerCase();
    //Sys.userAgent = ua;
    var s;
    if (s = ua.match(/msie ([\d.]+)/)) Sys.ie = s[1];
    if (s = ua.match(/firefox\/([\d.]+)/)) Sys.firefox = s[1];
    if (s = ua.match(/chrome\/([\d.]+)/)) Sys.chrome = s[1];
    if (s = ua.match(/opera.([\d.]+)/)) Sys.opera = s[1];
    if (s = ua.match(/micromessenger\/([\d.]+)/)) Sys.MicroMessenger = s[1];
    if (s = ua.match(/version\/([\d.]+).*safari/)) Sys.safari = s[1];
    window.Sys = Sys;

    window.webServices = "http://192.144.148.41/services/";



})(window);