
function FillDropDown(url, name, selectedValue = null, data = null, callback = null, addChooseOption = false) {

    $.ajax({
        type: 'GET',
        url: url,
        data: data,
        success: function (data) {

            $('[name="' + name + '"]').empty();

            if (addChooseOption) {
                $('[name="' + name + '"]').append($('<option>').text("Seçiniz").attr('value', '-1'));
            }

            $.each(data, function (index, value) {
                $('[name="' + name + '"]').append($('<option>').text(value.desc).attr('value', value.val));
            });

            if (selectedValue != null && selectedValue != "0" && selectedValue != "") {
                $('[name="' + name + '"]').val(selectedValue);
            }
            else {
                $('[name="' + name + '"]').selectedIndex = 0;
            }

        }
    }).done(function () {
        if (callback != null)
            callback();
    });;
}

function FillMultiDropDown(url, id, selectedValue = null, data = null, callback = null) {

    $.ajax({
        type: 'GET',
        url: url,
        data: data,
        success: function (data) {


            $('#' + id).empty();

            var selectedValues = [];
            if (selectedValue != null) {
                selectedValues = selectedValue.split(',');
            }


            $.each(data, function (index, value) {

                var isExists = selectedValues.includes(value.val.toString());

                if (isExists) {

                    $('#' + id).append($('<option>').text(value.desc).attr('value', value.val).attr('selected', true));
                }
                else {

                    $('#' + id).append($('<option>').text(value.desc).attr('value', value.val));
                }

            });


            $('#' + id).multiselect({
                enableFiltering: true,
                includeSelectAllOption: true,
                enableCaseInsensitiveFiltering: true,
                buttonWidth: '100%'
            });



        }
    }).done(function () {
        if (callback != null)
            callback();
    });;
}

function FillDropDownWithPrfx(url, name, data = null, callback = null) {

    var a = $(name);

    $.ajax({
        type: 'GET',
        url: url,
        data: data,
        success: function (data) {

            $(name).empty();
            $.each(data, function (index, value) {
                $(name).append($('<option>').text(value.desc).attr('value', value.val));
            });



        }
    }).done(function () {
        if (callback != null)
            callback();
    });;
}

function RenderDateTimePicker(selector, date) {

    var picker = $('#' + selector).datetimepicker({
        todayHighlight: true,
        autoclose: true,
        format: 'dd.mm.yyyy hh:ii',
    });

    $('#' + selector).on('changeDate', function (e) { $('[name="' + selector + '"]').val(moment(e.date).format()); })

    $('[name="' + selector + '"]').val(date);

    if (date === '') {
        picker.datetimepicker('setDate', new Date());
        $('[name="' + selector + '"]').val(moment(new Date()).format());

    } else {
        picker.datetimepicker('setDate', new Date(date));
        $('[name="' + selector + '"]').val(moment(new Date(date)).format());
    }
}

function RenderDatePicker(selector, date) {

    var picker = $('#' + selector).datepicker({
        todayHighlight: true,
        autoclose: true,
        format: 'dd.mm.yyyy',
    });

    $('[name="' + selector + '"]').val(moment(date).format());

    if (date === '') {
        picker.datepicker('setDate', new Date());
    } else {
        picker.datepicker('setDate', new Date(date));
    }
}

function ShowMessageBox(title, text, icon, callback) {

    Swal.fire({
        title: title,
        text: text,
        icon: icon,
        showCancelButton: true,
        confirmButtonText: "Evet",
        cancelButtonText: "Hayır"
    }).then(function (result) {
        if (result.value) {

            if (callback !== undefined && callback !== null) {
                callback();
            }

        }
    });

}

function GetFormatedDate(date) {

    var formatted = date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate() + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();

    return formatted;
}

var tagify;
function GenerateTagify() {

    $.ajax({
        type: 'GET',
        url: '/JsonObject/GetTicketLabels',
        success: function (data) {

            var input = document.getElementById('kt_tagify_5');
            tagify = new Tagify(input, {
                pattern: /^.{0,20}$/, // Validate typed tag(s) by Regex. Here maximum chars length is defined as "20"
                delimiters: ", ", // add new tags when a comma or a space character is entered
                maxTags: 6,
                keepInvalidTags: true, // do not remove invalid tags (but keep them marked as invalid)
                whitelist: data,
                transformTag: transformTag,
                dropdown: {
                    enabled: 3,
                }
            });

        }
    });



    function transformTag(tagData) {
        var states = [
            'success',
            'primary',
            'danger',
            'success',
            'warning',
            'dark',
            'primary',
            'info'];

        tagData.class = 'tagify__tag tagify__tag--' + states[KTUtil.getRandomInt(0, 7)];

        if (tagData.value.toLowerCase() == 'shit') {
            tagData.value = 's✲✲t'
        }
    }
}

function GetTagifyValue() {

    var retval = '';

    for (var i = 0; i < tagify.value.length; i++) {

        retval += tagify.value[i].value + ",";
    }

    return retval.slice(0, -1);
}

function ClearTags() {

    tagify.removeAllTags();
}

function AddTag(tags) {

    ClearTags();

    if (tags == null) return;

    var tagList = tags.split(',');

    for (var i = 0; i < tagList.length; i++) {

        tagify.addTags(tagList[i], false, true);
    }
}