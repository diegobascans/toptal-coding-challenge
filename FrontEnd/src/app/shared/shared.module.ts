import {NgModule} from '@angular/core';
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatIconModule} from "@angular/material/icon";
import {MatButtonModule} from "@angular/material/button";
import {MatTableModule} from "@angular/material/table";
import {YesNoPipe} from "./pipes/yes-no.pipe";
import {CommonModule} from "@angular/common";
import {MatSelectModule} from "@angular/material/select";
import {MatNativeDateModule, MatOptionModule} from "@angular/material/core";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";
import {MatDialogModule} from "@angular/material/dialog";
import {MatDatepickerModule} from "@angular/material/datepicker";
import {MatInputModule} from "@angular/material/input";
import {ReactiveFormsModule} from "@angular/forms";
import {FieldValidatorComponent} from "./components/field-validator/field-validator.component";
import {NgxMatDatetimePickerModule, NgxMatNativeDateModule} from "@angular-material-components/datetime-picker";
import {HttpClientModule} from "@angular/common/http";
import {NoPermissionPageComponent} from "./components/no-permission-page/no-permission-page.component";
import {MatCardModule} from "@angular/material/card";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {MatCheckboxModule} from "@angular/material/checkbox";

@NgModule({
  imports: [
    HttpClientModule,
    CommonModule,
  ],
  declarations: [
    YesNoPipe,
    FieldValidatorComponent,
    NoPermissionPageComponent,
  ],
  exports: [
    HttpClientModule,
    ReactiveFormsModule,
    MatInputModule,
    MatNativeDateModule,
    MatDatepickerModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatTableModule,
    MatSelectModule,
    MatOptionModule,
    MatFormFieldModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    YesNoPipe,
    CommonModule,
    FieldValidatorComponent,
    NgxMatNativeDateModule,
    NgxMatDatetimePickerModule,
    NoPermissionPageComponent,
    MatCardModule,
    MatSnackBarModule,
    MatCheckboxModule
  ],
})
export class SharedModule { }
