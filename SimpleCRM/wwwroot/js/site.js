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
        'Name': $('#TaskName').val(),
        'Executors': $('#TaskExecutors').val(),
        'ExecutionTime': $('#TaskExecutionTime').val(),
        'CompletionDate': $('#TaskCompletionDate').val(),
        'StateId': $('#TaskStateId').val(),
        'PlannedIntensity': $('#TaskPlannedIntensity').val(),
        'Description': $('#TaskDescription').val(),
        'ParentTaskId': parentTaskId
    }
}

function getSubtaskCreationDataAsJson(parentTaskId) {
    return {
        'Name': $('#SubtaskName').val(),
        'Executors': $('#SubtaskExecutors').val(),
        'ExecutionTime': $('#SubtaskExecutionTime').val(),
        'CompletionDate': $('#SubtaskCompletionDate').val(),
        'StateId': $('#SubtaskStateId').val(),
        'PlannedIntensity': $('#SubtaskPlannedIntensity').val(),
        'Description': $('#SubtaskDescription').val(),
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

function addSubtask(parentTaskId) {
    console.log(parentTaskId);

    const data = getSubtaskCreationDataAsJson(parentTaskId);

    postToController(data, "application/json", '/Home/CreateTask');

    $('#addSubtaskModal').modal('hide');
    LoadTaskTree();

    return false;
}

function editTask(taskId, parentTaskId) {
    const data = getTaskEditDataAsJson(taskId, parentTaskId);

    putToController(data, "application/json", '/Home/EditTask');
    LoadTaskTree();

    $('#editTaskModal').modal('hide');

    return false;
}

function LoadTaskInfo(id) {
    $("#main").load(`/Home/GetTaskInfo?id=${id}`);
}

function LoadTaskTree() {
    $("#myUL").load(`/Home/GetTaskTree`);

    var togglers = document.getElementByClassName("caret");

    for (var i = 0; i < togglers.length; i++) {
        togglers[i].addEventListener("click", function() {
            this.parentElement.querySelector(".nested").classList.toggle("active");
            this.classList.toggle("caret-down");
        });
    }
}

$(document).on('click', '.caret', function (event) {
    this.parentElement.querySelector(".nested").classList.toggle("active");
    this.classList.toggle("caret-down");
});