import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Photo } from 'src/app/_models/Photo';
import { FileUploader } from 'ng2-file-upload';
import { environment } from '../../../environments/environment';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
@Component({
  selector: 'app-photo-editer',
  templateUrl: './photo-editer.component.html',
  styleUrls: ['./photo-editer.component.css']
})
export class PhotoEditerComponent implements OnInit {
  @Input() photos: Photo[];
  @Output() getMemberChange = new EventEmitter<string>();
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseurl = environment.apiUrl;
  currentMain: Photo;
  constructor(private authService: AuthService, private userService: UserService, private alertify: AlertifyService) {}

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
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });
    this.uploader.onAfterAddingFile = file => {
      file.withCredentials = true;
    };
    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Photo = JSON.parse   (response);
        const photo: Photo = {
          id: res.id,
          url: res.url,
          dateAdded: res.dateAdded,
          discription: res.discription,
          isMain: res.isMain
        };

        this.photos.push(photo);
      }
    };
  }
  setMainPhoto(photo: Photo) {
    this.userService.setMainPhoto(this.authService.decodedToken.nameid, photo.id).subscribe(() => {
      this.currentMain = this.photos.filter(p => p.isMain === true)[0];
      this.currentMain.isMain = false;
      photo.isMain = true;
      this.authService.changeMemberPhoto(photo.url);
      this.authService.currentUser.photoUrl = photo.url;
      localStorage.setItem('user', JSON.stringify(this.authService.currentUser));
    }, error => {
      this.alertify.error(error);
    }
    );
  }
  deletePhoto(id: number) {
    this.alertify.comfirm('Are you sure ? photo will get delet', () => {
      this.userService.deletePhoto(this.authService.decodedToken.nameid, id).subscribe(() => {
        this.photos.splice(this.photos.findIndex(p => p.id === id), 1);
        this.alertify.success('Photo Have Been Deleted');
      }, error => {
        this.alertify.error('Unable to delete')
      });
    });
  }
}
