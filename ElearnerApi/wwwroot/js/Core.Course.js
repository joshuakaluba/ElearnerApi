var Core = Core || {};

( function ()
{
    try
    {
        this.Course = this.Course || {};

        ( function ()
        {
            this.GetLessonById = function ( lessonId)
            {
                try
                {
                    $.ajax({
                        url: '/Home/Lesson?id=' + lessonId,
                        type: 'GET',
                        contentType: "application/x-www-form-urlencoded; charset=utf-8",
                        dataType: "json",
                        success: function (response)
                        {
                            Core.HideLoadingModal();

                            console.log(response);

                            $("#lesson-title").text(response.name);
                            $("#lesson-content").empty();

                            var lessonContent = "<div id='accordion'>";

                            console.log(response.learningObjects);

                            var collapsed = "true";

                            for (var index = 0; index < response.learningObjects.length; index++)
                            {
                                console.log(index);

                                var learningObject = response.learningObjects[index];

                                var headerId = Core.GetGuid();

                                var collapseId = Core.GetGuid();

                                var decodedXml = Core.DecodeEscapedXml(learningObject.learningTask);

                                lessonContent += "<div class='card'><div class='card-header' id='" + headerId + "'><h5 class='mb-0'><button class='btn btn-link' data-toggle='collapse' data-target='#" + collapseId + "' aria-expanded='" + collapsed + "' aria-controls='collapseOne'><p style='color:black;'>" + learningObject.name + "</p></button></h5></div><div id='" + collapseId + "' class='collapse' aria-labelledby='" + headerId + "' data-parent='#accordion'><div class='card-body'> " + decodedXml + "</div></div></div>"

                                collapsed = "false";

                            }

                            lessonContent += "</div>";

                            console.log(lessonContent);

                            $("#lesson-content").append(lessonContent);

                        },
                        error: function ()
                        {
                            alert("error");
                        }
                    });
                }
                catch ( e )
                {
                    Core.ShowException( e.message );

                    console.error( e.message );
                }
            };

        }).apply(this.Course || {});
    }
    catch ( e )
    {
        console.error( e.message );
    }
}).apply(Core || {});


