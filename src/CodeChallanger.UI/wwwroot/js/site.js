// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.addEventListener('DOMContentLoaded', (event) => {
    hljs.highlightAll();

    document.querySelectorAll('.code-editor').forEach(editor => {
        // Загрузка сохраненного содержимого, если оно есть
        const challengeId = document.getElementById('challengeId').value;
        const savedContent = localStorage.getItem(challengeId);
        if (savedContent) {
            editor.textContent = savedContent;
            hljs.highlightElement(editor);
        }

        editor.addEventListener('input', () => {
            const restore = saveCaretPosition(editor);
            hljs.highlightElement(editor);
            restore();
            saveContentToLocal(editor, challengeId); // Сохраняем содержимое при каждом изменении
        });
    });

    function saveContentToLocal(editor, challengeId) {
        const content = editor.textContent;
        localStorage.setItem(challengeId, content);
    }

    function saveCaretPosition(context) {
        const selection = window.getSelection();
        const range = selection.getRangeAt(0);
        range.setStart(context, 0);
        const len = range.toString().length;

        return function restore() {
            const range = new Range();
            range.setStart(context, 0);
            range.setEnd(context, 0);

            const treeWalker = document.createTreeWalker(context, NodeFilter.SHOW_TEXT);

            let count = 0;
            while (treeWalker.nextNode()) {
                count += treeWalker.currentNode.length;
                if (count > len) {
                    range.setStart(treeWalker.currentNode, treeWalker.currentNode.length + len - count);
                    range.setEnd(treeWalker.currentNode, treeWalker.currentNode.length + len - count);
                    break;
                }
            }

            selection.removeAllRanges();
            selection.addRange(range);
        };
    }
});



 //document.querySelector('.code-editor').addEventListener('input', function () {

 //    console.log(this.textContent);

 //    localStorage.setItem(challengeId, this.textContent);
 //    // localStorage.setItem(challengeId, this.value);
 //});

 //document.addEventListener('DOMContentLoaded', function () {
 //    const savedCode = localStorage.getItem(challengeId);
 //    if (savedCode) {
 //        document.querySelector('.code-editor').textContent = savedCode;
 //    }
 //});

//document.querySelector('.code-editor').addEventListener('keydown', function (e) {
//    if (e.key === 'Tab' && !e.shiftKey) {
//        e.preventDefault();
//        var start = this.selectionStart;
//        var end = this.selectionEnd;

//        // Вставляем символ табуляции
//        this.value = this.value.substring(0, start) + '\t' + this.value.substring(end);

//        // Перемещаем курсор
//        this.selectionStart = this.selectionEnd = start + 1;
//    }
//});
