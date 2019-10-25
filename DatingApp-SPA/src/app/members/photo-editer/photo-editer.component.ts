import { Component, OnInit, Input } from '@angular/core';
import { Photo } from 'src/app/_models/Photo';
import { FileUploader } from 'ng2-file-upload';
import { environment } from '../../../environments/environment';
import { AuthService } from 'src/app/_services/auth.service';
@Component({
  selector: 'app-photo-editer',
  templateUrl: './photo-editer.component.html',
  styleUrls: ['./photo-editer.component.css']
})
export class PhotoEditerComponent implements OnInit {
  @Input() photos: Photo;
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseurl = environment.apiUrl;
  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.initialzeUploader();
  }
  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }
  initialzeUploader() {
    this.uploader = new FileUploader({
      url:
        this.baseurl +
        'users/' +
        this.authService.decodedToken.nameid +
        '/photos',
        authToken: 'Bearer' + localStorage.getItem('token'),
        isHTML5: true,
        allowedFileType:['image'],
        removeAfterUpload: true,
        autoUpload: false,
        maxFileSize: 10 * 1024 * 1024

    });
    this.uploader.onAfterAddingFile = (file) => {file.withCredentials = false; };
  }
}
