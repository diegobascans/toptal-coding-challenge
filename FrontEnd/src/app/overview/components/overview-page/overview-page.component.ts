import {Component, OnInit} from '@angular/core';
import {DATE_FORMAT} from "../../../common/constans/general.constants";
import {MatDialog} from "@angular/material/dialog";
import {NewMealComponent} from "../new-meal/new-meal.component";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {UserService} from "../../services/user.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {GetUserMealsResponse} from "../../services/method-parameters/get-user-meals-response";
import {CheatDietComponent} from "../cheat-diet/cheat-diet.component";
import {AuthenticationService} from "../../../shared/services/authentication.service";

interface CaloriesElement {
  day: Date,
  totalCalories: number,
  limitExceeded: boolean
}

@Component({
  selector: 'app-overview-page',
  templateUrl: './overview-page.component.html',
  styleUrls: ['./overview-page.component.scss']
})
export class OverviewPageComponent implements OnInit {

  readonly dateFormat = DATE_FORMAT;
  readonly maxDate = new Date();

  caloriesLimit: number;
  displayedColumns: string[] = ['day', 'totalCalories', 'limitExceeded'];
  dataSource: CaloriesElement[] = [];
  isLoading: boolean;

  dateRange: FormGroup;
  showMore: boolean;
  private pageNumber: number;


  constructor(
    private dialog: MatDialog,
    private userService: UserService,
    private authService: AuthenticationService,
    private snackBar: MatSnackBar) {
    const startDate = new Date();
    this.pageNumber = 1;
    startDate.setMonth(new Date().getMonth() - 1);
    this.dateRange = new FormGroup({
      start: new FormControl(startDate, Validators.required),
      end: new FormControl(this.maxDate, Validators.required),
    });
  }


  ngOnInit(): void {
    this.caloriesLimit = this.authService.userCaloriesLimit;
    this.pageNumber = 1;
    this.getUserMeals();
  }

  onAddNewMealClick() {
    const dialogRef = this.dialog.open(NewMealComponent, {
      data: {},
    });

    dialogRef.componentInstance.onAddedMeal.subscribe(() => {
      this.resetPagination();
      this.getUserMeals();
      this.snackBar.open('Meal added successfully', 'Ok', {
        horizontalPosition: 'center',
        verticalPosition: 'top',
        duration: 4000
      });
    })
  }

  onCheatDietClick() {
    const dialogRef = this.dialog.open(CheatDietComponent, {
      data: {},
    });

    dialogRef.componentInstance.onCheatedDiet.subscribe(() => {
      this.resetPagination();
      this.getUserMeals();
      this.snackBar.open('Cheated diet successfully', 'Ok', {
        horizontalPosition: 'center',
        verticalPosition: 'top',
        duration: 4000
      });
    })
  }

  public onDatesChangeEvent() {
    if(this.dateRange.valid) {
      this.resetPagination();
      this.getUserMeals();
    }
  }

  private getUserMeals() {
    this.isLoading = true;

    const startDate = this.dateRange.controls['start'].value;
    const endDate = this.dateRange.controls['end'].value;

    this.userService.getUserMeals(this.pageNumber, startDate, endDate).subscribe((res: GetUserMealsResponse) => {
      this.isLoading = false;

      const data: CaloriesElement[] = res.caloriesPerDay.map(m => {
        return {
          day: m.date,
          totalCalories: m.calories,
          limitExceeded: m.calories >= this.caloriesLimit
        }
      })

      this.dataSource = [...this.dataSource, ...data];

      if(this.dataSource.length < res.count){
        this.showMore = true;
      } else {
        this.showMore = false;
      }
    })
  }

  getField(fieldName: string): FormControl
  {
    return this.dateRange.controls[fieldName] as FormControl;
  }

  private resetPagination() {
    this.pageNumber = 1;
    this.dataSource = [];
  }

  getMoreRows() {
    this.pageNumber++;
    this.getUserMeals();
  }
}
