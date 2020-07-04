using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.HTTP.Routing
{
    class ResourceList
    {
        private List<Resource> resources;

        public ResourceList() {
            resources = new List<Resource>();
        }

        //<todo>
        //figure out validation
        //for adding the same resousces
        //</todo>
        public void AddResource(Resource resourceToAdd) {
            foreach (Resource resource in resources) {
                if (resource.GetResourceName().Equals(resourceToAdd.GetResourceName())) {
                    throw new ArgumentException("resource already exists");
                }
            }
            resources.Add(resourceToAdd);
        }

        public Resource GetResourceByName(string resourceName) {
            foreach(Resource resource in resources) {
                if (resource.GetResourceName().Equals(resourceName))
                {
                    return resource;
                }
            }
            throw new ArgumentException("resource already exists");
        }

        //<todo>
        //add search method
        //for method route
        //</todo>
    }
}
