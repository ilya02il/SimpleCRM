function postToController(data, dataType, url) {
    console.log('Submitting form...');
    console.log(data);
    $.ajax({
        method: 'POST',
        url: url,
        dataType: 'text',
        contentType: dataType,
        data: JSON.stringify(data),
        success: function (result) {
            LoadTaskTree();
        },
        failure: function (errMsg) {
            alert(errMsg);
        }
    });
}

function putToController(data, dataType, url) {
    console.log('Submitting form...');
    $.ajax({
        method: 'PUT',
        url: url,
        dataType: 'text',
        contentType: dataType,
        data: JSON.stringify(data),
        success: function (response) {
            LoadTaskInfo(data.Id);
            LoadTaskTree();
        },
        failure: function (errMsg) {
            alert(errMsg);
        }
    });
}

function getTaskCreationDataAsJson(parentTaskId) {
    return {
        'Name': $('#Name').val(),
        'Executors': $('#Executors').val(),
        'ExecutionTime': $('#ExecutionTime').val(),
        'CompletionDate': $('#CompletionDate').val(),
        'StateId': $('#StateId').val(),
        'PlannedIntensity': $('#PlannedIntensity').val(),
        'Description': $('#Description').val(),
        'ParentTaskId': parentTaskId
    }
}

function getTaskEditDataAsJson(taskId, parentTaskId) {
    return {
        'Id': taskId,
        'Name': $('#EditName').val(),
        'Executors': $('#EditExecutors').val(),
        'ExecutionTime': $('#EditExecutionTime').val(),
        'CompletionDate': $('#EditCompletionDate').val(),
        'StateId': $('#EditStateId').val(),
        'PlannedIntensity': $('#EditPlannedIntensity').val(),
        'Description': $('#EditDescription').val(),
        'ParentTaskId': parentTaskId
    }
}

function deleteTask(taskId) {
    $.ajax({
        url: '/Home/DeleteTask',
        method: 'delete',
        dataType: 'text',
        data: {
            id: taskId
        },
        success: function (response) {
            LoadTaskTree();
        },
    });
}

function addTask(parentTaskId) {
    console.log(parentTaskId);

    const data = getTaskCreationDataAsJson(parentTaskId);

    postToController(data, "application/json", '/Home/CreateTask');

    $('#addTaskModal').modal('hide');
    LoadTaskTree();

    return false;
}

function editTask(taskId, parentTaskId) {
    const data = getTaskEditDataAsJson(taskId, parentTaskId);

    putToController(data, "application/json", '/Home/EditTask');

    $('#editTaskModal').modal('hide');

    return false;
}

//$('#deleteTaskButton').click(function (taskId) {
//    const data = getTaskDataAsJson();

//    postToController(JSON.stringify(data), "application/json", '/Home/CreateTask');

//    $('#addTaskModal').modal('hide');
//    $("#myUL").load(`/Home/GetTaskTree`);

//    return false;
//});

function LoadTaskInfo(id) {
    $("#main").load(`/Home/GetTaskInfo?id=${id}`);
}

//function LoadCommonTaskInfo() {
//    $("#main").load(`/Home/Index`);
//}

function LoadTaskTree() {
    $("#myUL").load(`/Home/GetTaskTree`);

    $(document).on('click', '.caret', function (event) {
        this.parentElement.querySelector(".nested").classList.toggle("active");
        this.classList.toggle("caret-down");
    });
}

$('#addTaskModal').on('show.bs.modal', modalTitleChange);
        
function modalTitleChange (event) {
    var button = $(event.relatedTarget);
    var recipient = button.data('whatever');
    var modal = $(this);
    modal.find('.modal-title').text(recipient);
}

//function CreateTask(formId) {
//    const form = document.getElementById(formId);
//    form.submit();
//}

$(document).on('click', '.caret', function (event) {
    this.parentElement.querySelector(".nested").classList.toggle("active");
    this.classList.toggle("caret-down");
});