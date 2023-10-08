import {
  AbstractControl,
  ValidatorFn,
  FormGroup,
  FormArray,
  FormControl,
  Validators,
} from '@angular/forms';

export function emailListValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: any } | null => {
    const userListArray = control as FormArray;
    if (!userListArray || !Array.isArray(userListArray.controls)) {
      return null;
    }
    const invalidEmailIndex = userListArray.controls.findIndex(
      (userFormGroup) => {
        const emailControl = userFormGroup.get('emailId');
        return emailControl?.invalid && emailControl.touched;
      }
    );
    return invalidEmailIndex >= 0 ? { invalidEmail: true } : null;
    // if (!control.value) {
    //   return null;
    // }
    // const emailList = control.value.split('\n');
    // const invalidEmails = emailList.filter((email: string) => {
    //   const emailControl = new FormControl(email);
    //   return !emailControl.valid || !Validators.email(emailControl);
    // });

    // return invalidEmails.length ? { invalidEmailList: true } : null;
  };
}
