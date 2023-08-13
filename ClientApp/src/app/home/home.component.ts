import { HttpClient } from '@angular/common/http';
import { Component, HostListener } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor(private http: HttpClient,
    private router: Router) { }

  @HostListener('dragover', ['$event']) OnDragOver(evt: any) {
    evt.preventDefault();
    evt.stopPropagation();

  }

  @HostListener('dragleave', ['$event']) public OnDragLeave(event: any) {
    event.preventDefault();
    event.stopPropagation();
  }

  @HostListener('drop', ['$event']) public OnDrop(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();

    const files = event.dataTransfer?.files;
    if (files && files.length > 0) {
      const file = files[0];
      if (file.type.startsWith('image/')) {
        this.uploadImage(file);
      } else {
        alert('El archivo soltado no es una imagen válida.');
      }
    }
  }
  private uploadImage(img: File) {
    let formData = new FormData();
    formData.append('imagen', img);
    console.log(formData);
    this.router.navigateByUrl('/load-screen');

  }
}
