import {Component, Input} from "@angular/core";
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-field-validator',
  templateUrl: './field-validator.component.html',
  styleUrls: ['./field-validator.component.scss']
})
export class FieldValidatorComponent {

  @Input('displayMode')
  displayMode: string;

  private _fields: FormControl[];


  errorMessage: string;

  @Input('fields')
  set fields(value: FormControl[]) {
    this._fields = value;

    this._fields.forEach(field => {
      field.valueChanges.subscribe(() => {
        this.validate(field);
      })
    })
  }

  public validateField() {
    this._fields.forEach(field => {
      this.validate(field);
    });
  }

  private validate(field: FormControl) {
    this.errorMessage = '';
    if(field.dirty || field.touched) {
      if(field.errors) {
        Object.keys(field.errors).forEach(errorKey => {
          switch (errorKey){
            case 'matDatetimePickerParse':
            case 'matDatepickerParse':
              this.errorMessage = 'Invalid format.';
              break;
            case 'required':
              this.errorMessage = 'Field required.';
              break;
            case 'min':
              this.errorMessage = `Value must be higher than ${field.errors?.['min'].min - 1}.`
              break;
          }
        });
      }
    }
  }
}
