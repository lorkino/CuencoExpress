using System;
using System.Collections.Generic;
using System.Linq;
using Film.Models;

namespace Film.ViewModels
{
    public class ViewKnowledge
    {
        public string value;
        public string explanation;

        public static explicit operator ViewKnowledge(Knowledges v)
        {
            ViewKnowledge kwnoledge = new ViewKnowledge
            {
                value= v.Value,
                explanation = v.Explanation

            };
            return kwnoledge;
        }

       
    }
    //public class ViewKnowledgeUser
    //{
        
    //    public ViewKnowledge knowledge;

    //    public static explicit operator ViewKnowledgeUser(List<UserKnowledges> v)
    //    {
    //        //foreach (UserKnowledges element in v)
    //        //{
    //        //    ViewKnowledgeUser kwnoledge = new ViewKnowledgeUser
    //        //    {
    //        //        knowledge = (ViewKnowledge)element.Knowledges

    //        //    };
    //        //   yield  return kwnoledge;
    //        //}
    //        return null;

    //    }
    //}

}