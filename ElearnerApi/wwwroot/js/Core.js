﻿var Core = Core || {};

( function ()
{
    try
    {
        this.ShowMessage = function ( message, title )
        {
            if ( message )
            {
                if ( title )
                {
                    $( '#modal-show-message-default-title' ).text( title );
                }
                else
                {
                    $( '#modal-show-message-default-title' ).text( "Confirmation" );
                }

                $( '#modal-show-message-default-text' ).text( message );

                $( "#modal-show-message-default" ).modal( 'show' );
            }
        };
       
        this.ShowException = function ( message )
        {
            Core.ShowMessage( message, "Error" );
        };

        this.ShowLoadingModal = function ()
        {
            $( "#modal-loading" ).modal( 'show' );
        };

        this.HideLoadingModal = function ()
        {
            $( "#modal-loading" ).modal( 'hide' );

            setTimeout( function ()
            {
                $( "#modal-loading" ).modal( 'hide' );

            }, 500 );
        };

        this.ShowSuccessAlert = function ( message )
        {
            Core.ShowAlert( message, "alert-success" );
        };

        this.ShowDangerAlert = function ( message )
        {
            Core.ShowAlert( message, "alert-danger" );
        };

        this.ShowWarningAlert = function ( message )
        {
            Core.ShowAlert( message, "alert-warning" );
        };

        this.ShowViewAlert = function ( response )
        {
            if ( response && response.status )
            {
                var alertType = "alert-danger";

                if ( response.status.toLowerCase() == "success".toLowerCase() )
                {
                    alertType = "alert-success";
                }
                else if ( response.status.toLowerCase() == "danger".toLowerCase() )
                {
                    alertType = "alert-danger";
                }
                else if ( response.status.toLowerCase() == "warning".toLowerCase() )
                {
                    alertType = "alert-warning";
                }

                Core.ShowAlert( response.message, alertType );
            }
        };

        this.ShowAlert = function ( message, alertType = "alert-danger" )
        {
            $( '.alert-results-container' ).append( '<div class="alert-custom-message alert '
                + alertType + '"><a class="close" data-dismiss="alert">×</a><span><p class="text-center">'
                + message + '</p></span></div>' )

            setTimeout( function ()
            {
                $( ".alert-custom-message" ).remove();

            }, 3000 );
        };

        this.SetCookie = function ( cookieName, cookieValue, deleteCookie )
        {
            var expiryDays = 60;

            if ( deleteCookie )
            {
                expiryDays = -1;
                cookieValue = null;
            }

            var date = new Date();

            date.setTime( date.getTime() + ( expiryDays * 24 * 60 * 60 * 1000 ) );

            var expires = "expires=" + date.toUTCString();

            document.cookie = cookieName + "=" + cookieValue + ";" + expires + ";path=/";
        };

        this.GetCookie = function ( cookieName )
        {
            var name = cookieName + "=";

            var ca = document.cookie.split( ';' );

            for ( var i = 0; i < ca.length; i++ )
            {
                var cookie = ca[i];

                while ( cookie.charAt( 0 ) == ' ' )
                {
                    cookie = cookie.substring( 1 );
                }
                if ( cookie.indexOf( name ) == 0 )
                {
                    return cookie.substring( name.length, cookie.length );
                }
            }
            return "";
        };        

        this.GetGuid = function ()
        {
            try
            {
                return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace( /[xy]/g, function ( c )
                {
                    var r = Math.random() * 16 | 0, v = c == 'x' ? r : ( r & 0x3 | 0x8 );
                    return v.toString( 16 );
                } );
            }
            catch ( e )
            {
                Core.ShowException( e.message );

                console.error( e.message );
            }
        };

        this.DecodeEscapedXml = function ( xml )
        {
            try
            {
                var txt = document.createElement( "textarea" );

                txt.innerHTML = xml;

                return txt.value;

            }
            catch ( e )
            {
                Core.ShowException( e.message );

                console.error( e.message );
            }
        }

    }
    catch ( e )
    {
        Core.ShowException( e.message );

        console.error( e.message );
    }
} ).apply( Core || {} );

if ( !String.prototype.format )
{
    String.prototype.format = function ()
    {
        return this.replace( /{(\d+)}/g, function ( match, number )
        {
            return typeof arguments[number] != 'undefined' ? arguments[number] : match;
        } );
    };
}

