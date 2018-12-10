const _url = "api/todo";
let _todos = null;

function getCount(data)
{
    const element = $("#counter");
    let name = "item";
    if (data)
    {
        if (data > 1)
        {
            name = "items";
        }
        element.text(data + " " + name);
    }
    else
    {
        element.text("No " + name);
    }
}

$(document).ready(() =>
{
    getData();
});

function getData()
{
    $.ajax({
        type: "GET",
        url: _url,
        cache: false,
        success: function (data)
        {
            const tBody = $("#items");

            $(tBody).empty();

            getCount(data.length);

            $.each(data, function (key, item)
            {
                const tr = $("<tr></tr>")
                    .append(
                        $("<td></td>").append(
                            $("<input/>", {
                                type: "checkbox",
                                disabled: true,
                                checked: item.isComplete
                            })
                        )
                    )
                    .append($("<td></td>").text(item.name))
                    .append(
                        $("<td></td>").append(
                            $("<button>Edit</button>").on("click", function ()
                            {
                                EditItem(item.id);
                            })
                        )
                    )
                    .append(
                        $("<td></td>").append(
                            $("<button>Delete</button>").on("click", function ()
                            {
                                DeleteItem(item.id);
                            })
                        )
                    );

                tr.appendTo(tBody);
            });

            _items = data;
        }
    });
}

function AddItem()
{
    const item = {
        name: $("#add-name").val(),
        isComplete: false
    };

    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: _url,
        contentType: "application/json",
        data: JSON.stringify(item),
        error: (jqXHR, textStatus, errorThrown) =>
        {
            console.log("Something went wrong!");
        },
        success: (result) =>
        {
            getData();
            $("#add-name").val("");
        }
    });
}

function DeleteItem(id)
{
    $.ajax({
        url: _url + "/" + id,
        type: "DELETE",
        success: function (result)
        {
            getData();
        }
    });
}

function EditItem(id)
{
    $.each(_items, function (key, item)
    {
        if (item.id === id)
        {
            $("#edit-name").val(item.name);
            $("#edit-id").val(item.id);
            $("#edit-isComplete")[0].checked = item.isComplete;
        }
    });
    $("#spoiler").css({ display: "block" });
}

$(".my-form").on("submit", function ()
{
    const item = {
        name: $("#edit-name").val(),
        isComplete: $("#edit-isComplete").is(":checked"),
        id: $("#edit-id").val()
    };

    $.ajax({
        url: _url + "/" + $("#edit-id").val(),
        type: "PUT",
        accepts: "application/json",
        contentType: "application/json",
        data: JSON.stringify(item),
        success: function (result)
        {
            getData();
        }
    });

    closeInput();
    return false;
});

function closeInput()
{
    $("#spoiler").css({ display: "none" });
}