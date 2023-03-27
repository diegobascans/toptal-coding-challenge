import {Component, EventEmitter, OnInit, Output} from "@angular/core";
import {FormBuilder, FormControl, FormGroup} from "@angular/forms";
import {MatDialogRef} from "@angular/material/dialog";
import {UserService} from "../../services/user.service";
import {CheatedFood} from "../../classes/cheated-food";
import {finalize} from "rxjs";

@Component({
  selector: 'app-cheat-diet',
  templateUrl: './cheat-diet.component.html',
  styleUrls: ['./cheat-diet.component.scss']
})
export class CheatDietComponent implements OnInit {
  @Output('onCheatedDiet')
  onCheatedDiet: EventEmitter<void> = new EventEmitter<void>();

  loading: boolean;
  cheatedFoods: CheatedFood[] = [];
  saving: boolean = false;

  cheatFoodForm: FormGroup = new FormGroup({})

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<CheatDietComponent>,
    private userService: UserService
  ) {
  }

  ngOnInit(): void {
    this.loading = true;
    this.userService.getCheatedFoods().subscribe((cheatedFoods: CheatedFood[]) => {
      if(cheatedFoods) {
        this.loading = false;
        this.cheatedFoods = cheatedFoods;

        this.cheatedFoods.forEach(cf => {
          this.cheatFoodForm.addControl(`food_${cf.food.id}`, new FormControl(cf.cheated));
        })

      }
    })
  }

  onCancelClick(): void {
    this.dialogRef.close();
  }

  updateCheatingFood(): void {
    if(this.cheatFoodForm.valid) {
      this.saving = true;
      this.userService.updateCheatedFoods(this.cheatedFoods.filter(cf => cf.cheated).map(cf => cf.food)).pipe(finalize(() => {
        this.onCheatedDiet.emit();
        this.dialogRef.close();
      })).subscribe()

    }
  }
}
