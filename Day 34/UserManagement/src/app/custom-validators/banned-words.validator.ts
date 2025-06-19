import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function bannedWordsValidator(bannedWords: string[]): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value?.toLowerCase();
    if (bannedWords.some(word => value?.includes(word))) {
      return { bannedWord: true };
    }
    return null;
  };
}