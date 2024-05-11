mergeInto(LibraryManager.library, {

    Inited: function () {
        return inited();
    },

    GetLanguage: function () {
        var str = getLanguage();
        var bufferSize = lengthBytesUTF8(str) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(str, buffer, bufferSize);
        return buffer;
    },

    ShowInter: function (objectName, onCloseFunctionName, onErrorFunctionName) {
        showInter(UTF8ToString(objectName), UTF8ToString(onCloseFunctionName), UTF8ToString(onErrorFunctionName));
    },

    ShowVideo: function (objectName, onRewardedFunctionName, onFailedFunctionName, onClosedFunctionName) {
        showRewardedVideo(UTF8ToString(objectName), UTF8ToString(onRewardedFunctionName),
            UTF8ToString(onFailedFunctionName), UTF8ToString(onClosedFunctionName));
    },

    SaveExternal: function (data) {
        save(UTF8ToString(data))
    },

    LoadExternal: function (objectName, onLoadedFunctionName) {
        load(UTF8ToString(objectName), UTF8ToString(onLoadedFunctionName));
    },

    AddToFavorites: function () {
        addToFavorites();
    },
    JoinGroup: function (groupId) {
        joinGroup(groupId);
    },
    Recommend: function () {
        recommend();
    },

});