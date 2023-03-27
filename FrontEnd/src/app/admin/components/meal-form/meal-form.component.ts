import {Component, EventEmitter, Inject, Input, OnInit, Output} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {GeneralService} from "../../../shared/services/general.service";
import {AdminService} from "../../services/admin.service";
import {finalize, forkJoin, Observable, tap} from "rxjs";
import {Food} from "../../../common/classes/food";
import {User} from "../../../common/classes/user";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {NewMealComponent} from "../../../overview/components/new-meal/new-meal.component";
import {AdminElement} from "../../classes/admin-element";
import {ModeEnum} from "../../classes/mode.enum";
import {Meal} from "../../../common/classes/meal";

@Component({
  selector: 'app-meal-form',
  templateUrl: './meal-form.component.html',
  styleUrls: ['./meal-form.component.scss']
})
export class MealFormComponent implements OnInit {
  saving: boolean = false;
  loading: boolean = false;
  maxDate: Date = new Date()
  foods: Food[] = [];
  users: User[] = [];

  @Input('modalTitle')
  modalTitle: string;

  @Input('confirmButtonLabel')
  confirmButtonLabel: string;

  @Input('readOnly')
  readOnly: boolean;

  @Input('mode')
  mode: string;

  @Output('onChange')
  onChange: EventEmitter<void> = new EventEmitter<void>();

  mealForm: FormGroup;

  constructor(
    private generalService: GeneralService,
    private adminService: AdminService,
    private dialogRef: MatDialogRef<NewMealComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AdminElement
  ) {
  }

  ngOnInit(): void {
    const subscriptions: Observable<any>[] = [];
    this.loading = true;

    this.mealForm = new FormGroup({
      food: new FormControl({ value: undefined, disabled: this.readOnly }, Validators.required),
      user: new FormControl({ value: undefined, disabled: this.readOnly }, Validators.required),
      date: new FormControl({ value: new Date(), disabled: this.readOnly }, Validators.required),
      calories: new FormControl({ value: 0, disabled: this.readOnly }, [Validators.required, Validators.min(1)])
    });

    subscriptions.push(this.generalService.getFoods().pipe(tap((foods: Food[]) => {
      this.foods = foods;
    })));

    subscriptions.push(this.adminService.getUsers().pipe(tap((users: User[]) => {
      this.users = users;
    })));

    if(this.data) {
      this.mealForm.setValue({
        food: this.data.food,
        user: this.data.user,
        date: this.data.date,
        calories: this.data.calories
      })
    }

    forkJoin(subscriptions).subscribe(() => {
     this.loading = false;
    });

  }

  getField(fieldName: string): FormControl
  {
    return this.mealForm.controls[fieldName] as FormControl;
  }

  submitForm() {
    const formValues = this.mealForm.value;
    let input: Meal;
    switch (this.mode) {
      case ModeEnum.delete:
        this.adminService.deleteMeal(this.data.id).pipe(finalize(() => {
          this.onChange.emit();
          this.dialogRef.close();
        })).subscribe();
        break;
      case ModeEnum.edit:
        input = {
          id: this.data.id,
          date: formValues['date'],
          calories: formValues['calories'],
          foodId: formValues['food'].id,
          userId: formValues['user'].id

        }
        this.adminService.updateMeal(input).pipe(finalize(() => {
          this.onChange.emit();
          this.dialogRef.close();
        })).subscribe();
        break;
      case ModeEnum.create:
        input = {
          date: formValues['date'],
          calories: formValues['calories'],
          foodId: formValues['food'].id,
          userId: formValues['user'].id
        }
        this.adminService.createMeal(input).pipe(finalize(() => {
          this.onChange.emit();
          this.dialogRef.close();
        })).subscribe();
        break;
    }
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }

  compareCategoryObjects(obj1: Food | User, obj2: Food | User): boolean {
    return obj1 && obj2 && obj1.id === obj2.id;
  }
}
