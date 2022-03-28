/*
Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function( config )
{
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    //config.filebrowserBrowseUrl = "/uploads/files";
    //config.filebrowserWindowWidth = 500;
    //config.filebrowserWindowHeight = 650;
    //config.filebrowserUploadUrl = "/uploads/files";


    //Dosya Yöneticisi
    config.filebrowserBrowseUrl = '../fileman/index.html';// Öntanımlı Dosya Yöneticisi
    config.filebrowserImageBrowseUrl = '../fileman/index.html?type=image'; // Sadece Resim Dosyalarını Gösteren Dosya Yöneticisi
    config.removeDialogTabs = 'link:upload;image:upload'; // Upload işlermlerini dosya Yöneticisi ile yapacağımız için upload butonlarını kaldırıyoruz
    //---------

};
