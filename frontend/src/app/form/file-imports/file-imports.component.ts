import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-file-imports',
  imports: [],
  templateUrl: './file-imports.component.html',
  styleUrl: './file-imports.component.css',
})
export class FileImportsComponent {
  csvFile?: File;
  xmlFile?: File;
  previewData: any = [];

  constructor(private service: ApiService, private router: Router) {}

  onFileSelected(event: Event, type: 'xml' | 'csv') {
    const input = event.target as HTMLInputElement;
    const reader = new FileReader();
    this.previewData = [];

    if (input.files && input.files.length > 0) {
      switch (type) {
        case 'xml':
          this.xmlFile = input.files[0];

          reader.onload = () => {
            const parser = new DOMParser();
            const xmlDoc = parser.parseFromString(
              reader.result as string,
              'application/xml'
            );

            const cards = xmlDoc.getElementsByTagName('BusinessCardCsvXmlDto');
            for (let i = 0; i < cards.length; i++) {
              const card = cards[i];
              this.previewData.push({
                name: card.getElementsByTagName('Name')[0]?.textContent ?? '',
                email: card.getElementsByTagName('Email')[0]?.textContent ?? '',
                phone: card.getElementsByTagName('Phone')[0]?.textContent ?? '',
                gender:
                  card.getElementsByTagName('Gender')[0]?.textContent ?? '',
                dateOfBirth:
                  card.getElementsByTagName('DateOfBirth')[0]?.textContent ??
                  '',
                address:
                  card.getElementsByTagName('Address')[0]?.textContent ?? '',
                photo: card.getElementsByTagName('Photo')[0]?.textContent ?? '',
              });
            }
          };
          reader.readAsText(this.xmlFile);
          break;
        case 'csv':
          this.csvFile = input.files[0];

          reader.onload = () => {
            const text = reader.result as string;
            const lines = text
              .split('\n')
              .map((line) => line.trim())
              .filter((line) => line);
            const headers = lines[0].split(',');

            for (let i = 1; i < lines.length; i++) {
              const values = lines[i].split(',');

              this.previewData.push({
                name: values[headers.indexOf('Name')] ?? '',
                email: values[headers.indexOf('Email')] ?? '',
                phone: values[headers.indexOf('Phone')] ?? '',
                gender: values[headers.indexOf('Gender')] ?? '',
                dateOfBirth: values[headers.indexOf('DateOfBirth')] ?? '',
                address: values[headers.indexOf('Address')] ?? '',
                photo: values[headers.indexOf('Photo')] ?? '',
              });
            }
          };
          reader.readAsText(this.csvFile);
          break;
        default:
          console.log('none');
      }
    }
  }

  importFile(file: File | undefined, type: 'csv' | 'xml') {
    if (!file) return;

    const formData = new FormData();
    formData.append('file', file);

    const importObservable =
      type === 'csv'
        ? this.service.importCsv(formData)
        : this.service.importXml(formData);

    importObservable.subscribe(() => {
      this.router.navigate(['/list']);
    });
  }
}
