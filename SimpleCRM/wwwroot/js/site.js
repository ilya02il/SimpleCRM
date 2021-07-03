function LoadTaskInfo(id, type) {
    $("#main").load(`/Home/GetTaskInfo?id=${id}&type=${type}`);
}

var exampleModal = document.getElementById('addTaskModal');

exampleModal.addEventListener('show.bs.modal', function (event) {
    // Button that triggered the modal
    //var button = event.relatedTarget
    // Extract info from data-bs-* attributes
    //var recipient = button.getAttribute('data-bs-whatever')
    // If necessary, you could initiate an AJAX request here
    // and then do the updating in a callback.
    //
    // Update the modal's content.
    //var modalTitle = exampleModal.querySelector('.modal-title')
    //var modalBodyInput = exampleModal.querySelector('.modal-body input')

    //modalTitle.textContent = 'New message to ' + recipient
    //modalBodyInput.value = recipient
})

function CreateTask(formId) {
    const form = document.getElementById(formId);
    form.submit();
}

const carets = document.getElementsByClassName("caret");

for (let i = 0; i < carets.length; i++) {
    carets[i].addEventListener("click", function () {
        this.parentElement.querySelector(".nested").classList.toggle("active");
        this.classList.toggle("caret-down");
    });
}