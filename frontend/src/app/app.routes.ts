import { Routes } from '@angular/router';
import { ListComponent } from './list/list.component';
import { FormComponent } from './form/form.component';
import { FileImportsComponent } from './file-imports/file-imports.component';

export const routes: Routes = [
  {
    path: 'list',
    component: ListComponent,
  },
  {
    path: 'form',
    component: FormComponent,
  },
  {
    path: 'import',
    component: FileImportsComponent,
  },
];
