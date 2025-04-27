import { Component } from '@angular/core';
import { BusinessCard } from '../types/types';
import { ApiService } from '../services/api.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-list',
  imports: [CommonModule],
  templateUrl: './list.component.html',
  styleUrl: './list.component.css',
})
export class ListComponent {
  businessCards?: BusinessCard[];

  constructor(public service: ApiService) {}

  ngOnInit(): void {
    this.loadList();
  }

  loadList() {
    this.service.getAll().subscribe((businessCards) => {
      this.businessCards = businessCards;
    });
  }

  delete(id: number) {
    this.service.delete(id).subscribe(() => {
      this.loadList();
    });
  }

  exportCsv(id: number) {
    this.service.exportToCsv(id).subscribe((blob) => {
      this.downloadFile(blob, 'BusinessCard.csv');
    });
  }

  exportXml(id: number) {
    this.service.exportToXml(id).subscribe((blob) => {
      this.downloadFile(blob, 'BusinessCard.xml');
    });
  }

  downloadFile(blob: Blob, filename: string) {
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = filename;
    link.click();
  }
}
