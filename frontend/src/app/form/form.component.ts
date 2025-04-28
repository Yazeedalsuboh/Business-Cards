import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ApiService } from '../services/api.service';
import { Router } from '@angular/router';
import { FileImportsComponent } from './file-imports/file-imports.component';

@Component({
  selector: 'app-form',
  imports: [ReactiveFormsModule, CommonModule, FileImportsComponent],
  templateUrl: './form.component.html',
  styleUrl: './form.component.css',
})
export class FormComponent {
  form: FormGroup;
  selectedPhoto?: File;
  photoPreviewUrl: string | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private service: ApiService,
    private router: Router
  ) {
    this.form = this.formBuilder.group({
      name: [''],
      gender: [''],
      dateOfBirth: [''],
      email: [''],
      phone: [''],
      address: [''],
    });
  }

  onSubmit() {
    const formData = new FormData();
    const values = this.form.value;

    Object.keys(values).forEach((key) => {
      formData.append(key, values[key]);
    });

    if (this.selectedPhoto) {
      formData.append('photo', this.selectedPhoto);
    }

    this.service.add(formData).subscribe(() => {
      this.router.navigate(['/list']);
    });
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;

    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      this.selectedPhoto = file;

      if (file.type.startsWith('image/')) {
        const reader = new FileReader();
        reader.onload = () => {
          this.photoPreviewUrl = reader.result as string;
        };
        reader.readAsDataURL(file);
      }
    }
  }
}
