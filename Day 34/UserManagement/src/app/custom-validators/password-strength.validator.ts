import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function passwordStrengthValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    if (!value) return null;
    const hasNumber = /[0-9]/.test(value);
    const hasSymbol = /[!@#$%^&*]/.test(value);
    const isValidLength = value.length >= 6;
    if (!hasNumber || !hasSymbol || !isValidLength) {
      return { weakPassword: true };
    }
    return null;
  };
}