import {Component, EventEmitter, OnInit, Output} from "@angular/core";
import {OverviewService} from "../../services/overview.service";
import {Food} from "../../../common/classes/food";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {MatDialogRef} from "@angular/material/dialog";
import {UserService} from "../../services/user.service";
import {Meal} from "../../../common/classes/meal";
import {finalize} from "rxjs";

@Component({
  selector: 'app-new-meal',
  templateUrl: './new-meal.component.html',
  styleUrls: ['./new-meal.component.scss']
})
export class NewMealComponent implements OnInit {
  @Output('onAddedMeal')
  onAddedMeal: EventEmitter<void> = new EventEmitter<void>();
  loading: boolean;
  foods: Food[] = [];
  saving: boolean = false;
  maxDate: Date = new Date()

  newMealForm = new FormGroup({
    food: new FormControl({}, Validators.required),
    date: new FormControl(new Date(), Validators.required),
    calories: new FormControl(0, [Validators.required, Validators.min(1)])
  });

  getField(fieldName: string): FormControl
  {
    return this.newMealForm.controls[fieldName] as FormControl;
  }

  constructor(
    private overviewService: OverviewService,
    private dialogRef: MatDialogRef<NewMealComponent>,
    private userService: UserService
    ) {
  }

  ngOnInit(): void {
    this.foods = this.overviewService.foods;
    this.loading = true;
    this.overviewService.foodsUpdated.subscribe((updatedFoods) => {
      if(updatedFoods) {
        this.loading = false;
        this.foods = updatedFoods;
      }
    })
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }

  saveMeal() {
    if(this.newMealForm.valid) {
      const formValues = this.newMealForm.value;
      const meal: Meal = {
        foodId: formValues['food'].id,
        calories: formValues['calories'],
        date: formValues['date']
      }
      this.saving = true;
      this.userService.addMeal(meal).pipe(finalize(() => {
        this.onAddedMeal.emit();
        this.dialogRef.close();
      })).subscribe();
    }
  }
}
