using BepNhaMinh.Models;
using Microsoft.EntityFrameworkCore;

namespace BepNhaMinh.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(ApplicationDbContext db)
    {
        if (await db.Recipes.AnyAsync()) return;

        var categoryNames = new[] { "Món Việt", "Món nước", "Món chiên", "Món xào", "Món canh", "Món ăn sáng", "Món ăn trưa", "Món ăn tối", "Món ăn nhanh", "Món chay" };
        var categories = categoryNames.Select((name, i) => new Category { Name = name, Description = $"Các món thuộc nhóm {name.ToLowerInvariant()}.", ImageUrl = $"/images/recipes/category-{(i % 5) + 1}.svg" }).ToList();
        db.Categories.AddRange(categories);
        await db.SaveChangesAsync();

        var recipes = new (string Name,string Desc,string Category,int Prep,int Cook,int Serv,string Diff,int Cal,string[] Ing,string[] Steps)[]
        {
            ("Phở Bò","Món phở bò đậm đà với nước dùng thơm, thịt bò mềm và bánh phở dai.","Món nước",30,60,2,"Trung bình",520,["Bánh phở — 300g","Thịt bò — 200g","Hành tây — 50g","Hành lá — 30g","Gừng — 20g","Quế — 1 thanh"],["Rửa sạch thịt, rau và các gia vị.","Nướng thơm gừng, hành rồi cho vào nồi nước dùng.","Hầm nước dùng cùng quế và gia vị khoảng 45 phút.","Trụng bánh phở, xếp thịt bò rồi chan nước dùng nóng."]),
            ("Bún Chả","Bún chả Hà Nội với thịt nướng thơm, nước chấm chua ngọt và rau sống.","Món Việt",25,20,2,"Dễ",610,["Thịt ba chỉ — 300g","Thịt nạc vai — 200g","Bún tươi — 500g","Nước mắm — 60ml","Đường — 30g","Rau sống — 200g"],["Băm hoặc thái thịt rồi ướp với nước mắm, đường và tiêu.","Nặn thịt thành viên nhỏ và chuẩn bị thịt ba chỉ.","Nướng thịt đến khi vàng cạnh và dậy mùi thơm.","Pha nước chấm, dùng cùng bún và rau sống."]),
            ("Cơm Tấm","Đĩa cơm tấm quen thuộc với sườn nướng, bì, chả và nước mắm.","Món Việt",20,35,2,"Trung bình",680,["Gạo tấm — 300g","Sườn cốt lết — 300g","Trứng — 2 quả","Hành lá — 30g","Nước mắm — 50ml","Đồ chua — 100g"],["Vo gạo tấm và nấu chín.","Ướp sườn với nước mắm, đường, tỏi và mật ong.","Nướng sườn đến khi chín vàng hai mặt.","Làm mỡ hành và pha nước mắm chua ngọt rồi trình bày."]),
            ("Bánh Mì","Bánh mì Việt Nam giòn vỏ, nhân thịt và rau dưa cân bằng vị.","Món ăn nhanh",10,15,2,"Dễ",480,["Bánh mì — 2 ổ","Thịt nguội — 150g","Pate — 80g","Dưa leo — 1 quả","Đồ chua — 100g","Rau mùi — 20g"],["Chuẩn bị bánh mì và các loại rau.","Phết pate và sốt lên mặt trong bánh.","Xếp thịt nguội, dưa leo và đồ chua.","Thêm rau mùi, kẹp bánh và dùng ngay."]),
            ("Bún Bò Huế","Bún bò Huế cay thơm, nước dùng đậm đà với sả và mắm ruốc.","Món nước",35,70,3,"Khó",590,["Bún sợi to — 500g","Bắp bò — 300g","Giò heo — 400g","Sả — 5 cây","Mắm ruốc — 30g","Ớt — 2 quả"],["Sơ chế và chần thịt, giò heo.","Nấu nước dùng cùng sả và hành.","Nêm mắm ruốc, ớt và gia vị cho vừa ăn.","Trụng bún, xếp thịt rồi chan nước dùng nóng."]),
            ("Mì Quảng","Mì Quảng với sợi mì vàng, tôm thịt và nước dùng ít nhưng đậm vị.","Món Việt",20,30,2,"Trung bình",560,["Mì Quảng — 400g","Tôm — 200g","Thịt heo — 200g","Đậu phộng — 50g","Rau sống — 150g","Bánh tráng — 2 cái"],["Sơ chế tôm và thịt.","Xào tôm thịt với hành và gia vị.","Thêm nước dùng và nấu nhỏ lửa.","Trụng mì, cho nhân và ít nước dùng, rắc đậu phộng."]),
            ("Gỏi Cuốn","Gỏi cuốn tươi mát với tôm, thịt, rau và bún cuộn trong bánh tráng.","Món Việt",25,10,2,"Dễ",330,["Bánh tráng — 10 cái","Tôm — 200g","Thịt ba chỉ — 150g","Bún — 200g","Xà lách — 100g","Hẹ — 20g"],["Luộc tôm và thịt rồi thái mỏng.","Nhúng bánh tráng cho mềm.","Xếp rau, bún, thịt và tôm lên bánh.","Cuộn chặt tay, dùng cùng nước chấm."]),
            ("Cơm Chiên","Cơm chiên hạt rời, thơm trứng và rau củ, phù hợp bữa ăn nhanh.","Món chiên",10,15,2,"Dễ",510,["Cơm nguội — 400g","Trứng — 2 quả","Cà rốt — 50g","Đậu Hà Lan — 50g","Hành lá — 20g","Nước tương — 20ml"],["Chuẩn bị cơm nguội và rau củ.","Xào trứng vừa chín rồi để riêng.","Đảo cơm với rau củ trên lửa lớn.","Cho trứng, hành lá và nước tương, đảo đều."]),
            ("Thịt Kho Trứng","Thịt kho trứng mềm béo, nước màu trong và vị mặn ngọt hài hòa.","Món ăn tối",20,60,4,"Trung bình",720,["Thịt ba chỉ — 500g","Trứng gà — 4 quả","Nước dừa — 500ml","Nước mắm — 50ml","Đường — 40g","Hành tím — 30g"],["Luộc trứng, bóc vỏ và cắt thịt thành miếng.","Thắng đường tạo màu caramel.","Cho thịt vào đảo săn rồi thêm nước dừa.","Thả trứng và kho nhỏ lửa đến khi mềm, nước sánh."]),
            ("Cá Kho Tộ","Cá kho tộ thơm tiêu, nước màu và nước mắm, ăn rất hợp với cơm nóng.","Món ăn trưa",15,45,3,"Dễ",460,["Cá lóc — 500g","Thịt ba chỉ — 100g","Nước mắm — 50ml","Đường — 30g","Tiêu — 5g","Hành lá — 20g"],["Làm sạch cá và ướp với nước mắm, tiêu.","Thắng đường tạo nước màu.","Xếp cá và thịt vào tộ, thêm nước màu.","Kho nhỏ lửa đến khi cá thấm và nước keo lại."]),
            ("Canh Chua Cá","Canh chua cá thanh mát với me, cà chua, dứa và rau thơm.","Món canh",15,20,3,"Dễ",280,["Cá — 400g","Cà chua — 2 quả","Dứa — 150g","Đậu bắp — 100g","Me — 30g","Rau om — 20g"],["Sơ chế cá và rau củ.","Nấu nước với me rồi lọc lấy vị chua.","Cho cá, cà chua và dứa vào nấu chín.","Thêm đậu bắp, rau om và nêm lại gia vị."]),
            ("Gà Chiên Nước Mắm","Gà chiên giòn áo sốt nước mắm tỏi thơm đậm đà.","Món chiên",20,25,3,"Trung bình",650,["Cánh gà — 600g","Nước mắm — 40ml","Đường — 25g","Tỏi — 30g","Bột chiên — 100g","Ớt — 1 quả"],["Rửa và ướp cánh gà với gia vị.","Áo một lớp bột mỏng.","Chiên gà vàng giòn rồi để ráo dầu.","Phi tỏi, thêm nước mắm đường rồi đảo gà trong sốt."]),
            ("Sườn Xào Chua Ngọt","Sườn mềm phủ sốt chua ngọt, màu đẹp và dễ ăn với cơm.","Món xào",20,35,3,"Trung bình",620,["Sườn non — 500g","Dứa — 100g","Cà chua — 2 quả","Giấm — 20ml","Đường — 30g","Hành tỏi — 30g"],["Chặt sườn vừa ăn và chần sơ.","Chiên áp chảo sườn cho vàng.","Nấu sốt từ cà chua, đường và giấm.","Cho sườn vào đảo cùng sốt và dứa đến khi sệt."]),
            ("Thịt Rang Cháy Cạnh","Thịt ba chỉ rang cháy cạnh thơm giòn, mặn ngọt vừa miệng.","Món ăn trưa",10,20,3,"Dễ",690,["Thịt ba chỉ — 400g","Nước mắm — 30ml","Đường — 20g","Hành tím — 20g","Tiêu — 5g","Hành lá — 20g"],["Thái thịt thành miếng mỏng.","Áp chảo thịt cho ra mỡ và xém cạnh.","Thêm nước mắm, đường và hành tím.","Đảo đến khi nước sốt bám quanh miếng thịt, rắc tiêu."]),
            ("Trứng Chiên","Trứng chiên vàng mềm, nhanh gọn và phù hợp mọi bữa cơm.","Món ăn sáng",5,5,2,"Dễ",220,["Trứng gà — 3 quả","Hành lá — 20g","Nước mắm — 10ml","Tiêu — 2g","Dầu ăn — 15ml"],["Đập trứng vào bát và đánh tan.","Thêm hành lá, nước mắm và tiêu.","Làm nóng chảo với dầu.","Đổ trứng, chiên vàng hai mặt và dùng nóng."]),
            ("Rau Muống Xào Tỏi","Rau muống xanh giòn xào tỏi thơm, món phụ quen thuộc của gia đình.","Món xào",10,8,2,"Dễ",180,["Rau muống — 400g","Tỏi — 30g","Dầu ăn — 15ml","Nước mắm — 10ml","Muối — 3g"],["Nhặt rau và rửa sạch.","Chần rau nhanh trong nước sôi rồi để ráo.","Phi thơm tỏi với dầu.","Xào rau lửa lớn, nêm gia vị và tắt bếp."]),
            ("Đậu Hũ Sốt Cà Chua","Đậu hũ mềm sốt cà chua chua nhẹ, thanh và dễ nấu.","Món chay",10,15,2,"Dễ",260,["Đậu hũ — 400g","Cà chua — 3 quả","Hành lá — 20g","Nước tương — 20ml","Đường — 10g","Dầu ăn — 15ml"],["Cắt đậu hũ thành miếng vừa ăn.","Chiên sơ đậu hũ cho vàng mặt.","Xào cà chua với dầu và gia vị.","Cho đậu hũ vào sốt, nấu nhỏ lửa và thêm hành lá."]),
            ("Cháo Gà","Cháo gà nóng mềm, thơm gừng và hành, thích hợp bữa sáng hoặc ngày mệt.","Món ăn sáng",15,45,3,"Dễ",390,["Gạo — 150g","Thịt gà — 300g","Gừng — 20g","Hành lá — 20g","Tiêu — 3g","Nước — 1.5 lít"],["Vo gạo và sơ chế thịt gà.","Luộc gà cùng gừng, sau đó vớt gà ra xé nhỏ.","Cho gạo vào nước luộc và nấu đến khi nở mềm.","Cho thịt gà vào cháo, nêm và thêm hành tiêu."]),
            ("Bún Thịt Nướng","Bún thịt nướng cân bằng vị ngọt, mặn, chua cùng rau sống và đậu phộng.","Món Việt",25,20,2,"Trung bình",570,["Thịt heo — 300g","Bún — 400g","Rau sống — 150g","Đậu phộng — 40g","Nước mắm — 40ml","Đồ chua — 100g"],["Thái thịt mỏng và ướp gia vị.","Nướng thịt đến khi vàng thơm.","Chuẩn bị bún, rau và đồ chua.","Xếp bún, rau, thịt nướng, đậu phộng và chan nước mắm."]),
            ("Lẩu Thái","Lẩu Thái chua cay với hải sản, nấm và rau, phù hợp bữa ăn nhóm.","Món ăn tối",25,35,4,"Trung bình",540,["Tôm — 300g","Mực — 250g","Nấm — 200g","Cà chua — 2 quả","Sả — 3 cây","Nước cốt chanh — 30ml"],["Sơ chế hải sản, nấm và rau.","Nấu nước dùng với sả, cà chua và gia vị.","Nêm vị chua cay bằng chanh và ớt.","Cho hải sản và nấm vào nhúng chín, dùng nóng."])
        };

        var recipeIndex = 0;
        foreach (var item in recipes)
        {
            recipeIndex++;
            var category = categories.First(c => c.Name == item.Category);
            var recipe = new Recipe { Name = item.Name, Description = item.Desc, CategoryId = category.Id, ImageUrl = $"/images/recipes/recipe-{recipeIndex}.svg", PreparationTime = item.Prep, CookingTime = item.Cook, Servings = item.Serv, Difficulty = item.Diff, Calories = item.Cal, CreatedAt = DateTime.UtcNow };
            db.Recipes.Add(recipe);
            foreach (var text in item.Ing)
            {
                var parts = text.Split(" — ", 2);
                var ingredient = await db.Ingredients.FirstOrDefaultAsync(x => x.Name == parts[0]);
                if (ingredient is null) { ingredient = new Ingredient { Name = parts[0], Unit = parts.Length > 1 ? parts[1] : "" }; db.Ingredients.Add(ingredient); await db.SaveChangesAsync(); }
                recipe.RecipeIngredients.Add(new RecipeIngredient { IngredientId = ingredient.Id, Quantity = parts.Length > 1 ? parts[1] : "" });
            }
            recipe.CookingSteps = item.Steps.Select((s, i) => new CookingStep { StepNumber = i + 1, Title = new[] { "Chuẩn bị nguyên liệu", "Chế biến", "Nấu và hoàn thiện", "Trình bày" }[i], Description = s }).ToList();
        }
        await db.SaveChangesAsync();
    }

}
