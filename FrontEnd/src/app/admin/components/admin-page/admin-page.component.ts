import {Component, OnInit} from '@angular/core';
import {AdminService} from "../../services/admin.service";
import {DATE_TIME_FORMAT} from "../../../common/constans/general.constants";
import {GeneralInformationResponse} from "../../services/method-parameters/general-information-response";
import {MatDialog} from "@angular/material/dialog";
import {MealFormComponent} from "../meal-form/meal-form.component";
import {AdminElement} from "../../classes/admin-element";
import {ModeEnum} from "../../classes/mode.enum";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {UserService} from "../../../overview/services/user.service";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-admin-page',
  templateUrl: './admin-page.component.html',
  styleUrls: ['./admin-page.component.scss']
})
export class AdminPageComponent implements OnInit{
  dataSource: AdminElement[] = [];
  isLoading: boolean = false;
  dateFormat = DATE_TIME_FORMAT;
  displayedColumns: string[] = ['date', 'user', 'food', 'calories', 'actions'];
  showMore: boolean = false;

  readonly maxDate = new Date();
  dateRange: FormGroup;
  private pageNumber = 1;

  constructor(private dialog: MatDialog, private adminService: AdminService, private snackBar: MatSnackBar) {
    const startDate = new Date();
    startDate.setMonth(new Date().getMonth() - 1);
    this.dateRange = new FormGroup({
      start: new FormControl(startDate, Validators.required),
      end: new FormControl(this.maxDate, Validators.required),
    });
  }

  ngOnInit(): void {
    this.loadGeneralInformation();
  }

  getMoreRows() {
    this.pageNumber++;
    this.loadGeneralInformation();
  }

  private loadGeneralInformation() {
    this.isLoading = true;

    const startDate = this.dateRange.controls['start'].value;
    const endDate = this.dateRange.controls['end'].value;

    this.adminService.getGeneralInformation(this.pageNumber, startDate, endDate).subscribe((res:GeneralInformationResponse) => {
      this.isLoading = false;

      const data: AdminElement[] = res.meals.map(m => {
        return {
          id: m.id != undefined ? m.id : 0,
          date: m.date,
          calories: m.calories,
          user: m.user,
          food: m.food,
        }
      })

      this.dataSource = [...this.dataSource, ...data];

      if(this.dataSource.length < res.totalMeals){
        this.showMore = true;
      } else {
        this.showMore = false;
      }
    })
  }

  onDeleteItem(element: AdminElement) {
    const dialogRef = this.dialog.open(MealFormComponent, {
      data: element,
    });

    dialogRef.componentInstance.modalTitle = 'Are you sure you want to delete the record?';
    dialogRef.componentInstance.confirmButtonLabel = 'Delete';
    dialogRef.componentInstance.readOnly = true;
    dialogRef.componentInstance.mode = ModeEnum.delete;
    dialogRef.componentInstance.onChange.subscribe(() => {
      this.showAlert('Meal deleted successfully');
      this.resetPagingInfo();
      this.loadGeneralInformation();
    })
  }

  onViewItem(element: AdminElement) {
    const dialogRef = this.dialog.open(MealFormComponent, {
      data: element,
    });

    dialogRef.componentInstance.modalTitle = 'Record details';
    dialogRef.componentInstance.readOnly = true;
    dialogRef.componentInstance.mode = ModeEnum.viewDetail;
  }

  onUpdateItem(element: AdminElement) {
    const dialogRef = this.dialog.open(MealFormComponent, {
      data: element,
    });

    dialogRef.componentInstance.modalTitle = 'Update record';
    dialogRef.componentInstance.confirmButtonLabel = 'Update';
    dialogRef.componentInstance.mode = ModeEnum.edit;
    dialogRef.componentInstance.onChange.subscribe(() => {
      this.showAlert('Meal updated successfully');
      this.resetPagingInfo();
      this.loadGeneralInformation();
    })
  }

  onAddNewMealClick() {
    const dialogRef = this.dialog.open(MealFormComponent);

    dialogRef.componentInstance.modalTitle = 'New record';
    dialogRef.componentInstance.confirmButtonLabel = 'Create';
    dialogRef.componentInstance.mode = ModeEnum.create;
    dialogRef.componentInstance.onChange.subscribe(() => {
      this.showAlert('Meal added successfully');
      this.resetPagingInfo();
      this.loadGeneralInformation();
    })
  }

  public onDatesChangeEvent() {
    if(this.dateRange.valid) {
      this.resetPagingInfo();
      this.loadGeneralInformation();
    }
  }

  getField(fieldName: string): FormControl
  {
    return this.dateRange.controls[fieldName] as FormControl;
  }

  private resetPagingInfo(): void  {
    this.dataSource = [];
    this.pageNumber = 1;
  }

  private showAlert(message: string) {
    this.snackBar.open(message, 'Ok', {
      horizontalPosition: 'center',
      verticalPosition: 'top',
      duration: 4000
    });
  }
}
