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
    this.service.getAll().subscribe((businessCards) => {
      this.businessCards = businessCards;
    });
  }
}
