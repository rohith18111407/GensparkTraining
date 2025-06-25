export class RecipeModel{
    constructor(
        public id:number=0,
        public name:string='',
        public ingredients:string[]=[],
        public instructions:String[]=[],
        public cuisine:string='',
        public image:string='',
        public prepTimeMinutes:number=0
    ){}

    static fromResponse(data:any){
        return new RecipeModel(
            data.id,
            data.name,
            data.ingredients,
            data.instructions,
            data.cuisine,
            data.image,
            data.prepTimeMinutes
        )
    }
}   