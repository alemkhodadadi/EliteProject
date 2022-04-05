using EliteBlog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteBlog.Repository.Seeds
{
    public class SeedPosts
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<Post>().HasData(
                    new Post()
                    {
                        ID = Guid.NewGuid(),
                        Title = "14 Ways To Help Students Build Confidence",
                        Text = "Once it’s begun, it’s difficult to fully separate the person from the task. When the artist is painting,the painter and the act of painting become a single ‘thing.’ The painting becomes a part of it all,too. As a teacher, your ‘self’ is embedded within your teaching—which is how it goes from ‘job’ to craft. The learning results are yours. You probably call them ‘your’ students. The same goes for students as well. There is a pleasing kind of string between the 8-year-old playing Minecraft and his or her digital creation. But this also presents some problems. The work and performance of students—both what they can and can’t do—is a part of who they are, and they are keenly aware of this. Even our language reflects this idea.Did you do your best on your homework? (As opposed to “Was the best work done on the assigned homework?”) Are you an A student? (As opposed to a student that usually receives As on their report card.) Are you confused? (As opposed to awkward sounding but entirely logical “Do you have confusion?”) Learning is personal.",
                    },
                    new Post()
                    {
                        ID = Guid.NewGuid(),
                        Title = "Trump’s Mental State Is Deteriorating Fast As He Now Thinks He Is Not A Civilian",
                        Text = "At a White House event, President Trump declared himself no longer a civilian as he seems to believe that being president also makes him a member of the military.",
                    },
                    new Post()
                    {
                        ID = Guid.NewGuid(),
                        Title = "7 Contemporary Horror Novels that Push Boundaries",
                        Text = "the grocery store of all places was my initial indoctrination into the world of horror. As my father shuffled up and down the aisles, dutifully stacking groceries in the cart for our family, I would sneak away to the magazine section and my eye was always drawn to the shiny paperback display brimming with such creepy covers as Salem’s Lot, The Legacy, and Flowers in the Attic. At first, I was too frightened to even touch the books. My young mind was convinced whatever horrors lurked behind those monolithic and terrifying covers would surely emerge from the pages and follow me home to stalk me at night. But as I grew older, just as Lucky Charms were a staple of my grocery booty as a kid, mass market horror novels found their way into my diet as an early teen. My love for the genre has only grown in time, and my tastes in horror have become vast. Lately, I have been craving new voices and favoring authors who are not afraid to take risks, push boundaries, and speak bravely from their own unique perspective. More importantly, I enjoy reading from voices that have a unique or daring tone that breaks the mold and pushes the horror genre into interesting and new paradigms—everything from classic monster scares, to psychological horror, to shivering Gothic tales. These are my seven favorite horror novels from boundary pushing authors with bold and unique voices. ",
                    },
                    new Post()
                    {
                        ID = Guid.NewGuid(),
                        Title = "VEGAN OMEGA 3 SUPPLEMENTS: 3 REASONS WHY PLANT-BASED IS THE WAY FORWARD",
                        Text = "if you’re vegan, you’ll probably know that getting all the nutrients your body needs can be a challenge. Omega 3 fatty acids are no exception since they are essential for maintaining heart and brain health, as well as:The problem is that the body can’t produce omega 3 fatty acids on its own, so you need to obtain them through food or supplements. However, both vegans and non-vegans alike often fall short in their daily intake of fatty acids EPA (eicosapentaenoic acid) and DHA (docosahexaenoic acid). Fish oil supplements have long been a popular way to bridge the gap, but they’re ‘off the menu’ if you’re on a plant-based diet. Although, it’s just as well, considering the many health and environmental risks associated with fish oil, including large unsustainability and possible contamination [1]. Thankfully, plant-based alternatives are becoming increasingly available, providing all the health benefits of omega 3 with none of the drawbacks. In this blog post, we’ll talk about three reasons you should consider making a plant-based omega 3 supplement part of your daily supplement routine",
                    }
                );
        }
    }
}
