window.$ = jQuery;
/*-----------------------------------------------------------------------------------*/
/*	POSTS GRID
/*-----------------------------------------------------------------------------------*/ 
$(window).load(function(){
    var $container = $('.blog-grid');

    var gutter = 30;
    var min_width = 345;
    $container.imagesLoaded( function(){
        $container.masonry({
            itemSelector : '.post',
            gutterWidth: gutter,
            isAnimated: true,
              columnWidth: function( containerWidth ) {
                var box_width = (((containerWidth - gutter)/2) | 0) ;

                if (box_width < min_width) {
                    box_width = (((containerWidth - gutter)/2) | 0);
                }

                if (box_width < min_width) {
                    box_width = containerWidth;
                }

                $('.post').width(box_width);

                return box_width;
              }
        });
        $container.css( 'visibility', 'visible' ).parent().removeClass( 'loading' );
    });
});



/*-----------------------------------------------------------------------------------*/
/*	FORM
/*-----------------------------------------------------------------------------------*/
jQuery(document).ready(function($) {
	$('.forms').dcSlickForms();
});


jQuery(document).ready(function($) {
	$('.comment-form input[title]').each(function() {
		if($(this).val() === '') {
			$(this).val($(this).attr('title'));	
		}
		
		$(this).focus(function() {
			if($(this).val() == $(this).attr('title')) {
				$(this).val('').addClass('focused');	
			}
		});
		$(this).blur(function() {
			if($(this).val() === '') {
				$(this).val($(this).attr('title')).removeClass('focused');	
			}
		});
	});
});

/*-----------------------------------------------------------------------------------*/
/*	VIDEO
/*-----------------------------------------------------------------------------------*/

jQuery(document).ready(function($) {
    		$('.video').fitVids();
    	});	

/*-----------------------------------------------------------------------------------*/
/*	FLEXSLIDER
/*-----------------------------------------------------------------------------------*/

jQuery(document).ready(function($){
      $('.blog-grid .slider').flexslider({
        animation: "fade",
        controlNav: true,
        animationLoop: true,
        slideshow: false,
        smoothHeight: false
      });
    });

$(window).load(function(){
      $('.single .slider').flexslider({
        animation: "fade",
        controlNav: true,
        animationLoop: true,
        slideshow: false,
        smoothHeight: true
      });
    });
    
/*-----------------------------------------------------------------------------------*/
/*	BUTTON HOVER
/*-----------------------------------------------------------------------------------*/

jQuery(document).ready(function($)  {
$("a.button, .forms fieldset .btn-submit, #commentform input#submit").css("opacity","1.0");
$("a.button, .forms fieldset .btn-submit, #commentform input#submit").hover(function () {
$(this).stop().animate({ opacity: 0.85 }, "fast");  },
function () {
$(this).stop().animate({ opacity: 1.0 }, "fast");  
}); 
});

/*-----------------------------------------------------------------------------------*/
/*	IMAGE HOVER
/*-----------------------------------------------------------------------------------*/		
		
jQuery(document).ready(function($) {	
$('.quick-flickr-item').addClass("frame");
$('.frame a').prepend('<span class="more"></span>');
});

jQuery(document).ready(function($) {
        $('.frame').mouseenter(function(e) {

            $(this).children('a').children('span').fadeIn(300);
        }).mouseleave(function(e) {

            $(this).children('a').children('span').fadeOut(200);
        });
    });	

/*-----------------------------------------------------------------------------------*/
/*	TOGGLE AND TABS
/*-----------------------------------------------------------------------------------*/

jQuery(document).ready(function($) {
//Hide the tooglebox when page load
$(".togglebox").hide();
//slide up and down when click over heading 2
$("h4").click(function(){
// slide toggle effect set to slow you can set it to fast too.
$(this).toggleClass("active").next(".togglebox").slideToggle("slow");
return true;
});
});

// perform JavaScript after the document is scriptable.
jQuery(document).ready(function($) {
	// setup ul.tabs to work as tabs for each div directly under div.panes
	$("ul.tabs").tabs("div.panes > div", {effect: 'fade'});
});



/*-----------------------------------------------------------------------------------*/
/*	AUDIO PLAYER
/*-----------------------------------------------------------------------------------*/

$(window).load(function(){
		$('.blog-grid audio').mediaelementplayer({
			audioWidth: '100%',
			features: ['playpause','progress','tracks'],
			videoVolume: 'horizontal'
		});
	});
	
jQuery(document).ready(function($) {
		$('.single audio').mediaelementplayer({
			audioWidth: '100%',
			features: ['playpause','progress','tracks'],
			videoVolume: 'horizontal'
		});
	});
	