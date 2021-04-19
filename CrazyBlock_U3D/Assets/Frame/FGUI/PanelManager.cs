using System;
using System.Collections.Generic;

namespace Frame.FGUI
{
    public class PanelManager:Singleton<PanelManager>, ISingleton
    {
        private List<SPanel> panels = new List<SPanel>();

        public bool HasPanel()
        {
            return panels.Count > 0;
        }
        
        public T Open<T>() where T : SPanel, new()
        {
            T panel = new T();
            panel.Init();
            return panel;
        }

        public T GetPanel<T>() where T : SPanel
        {
            Type type = typeof(T);
            SPanel ui = panels.Find(panel => type == panel.GetType());
            return (T) ui;
        }

        [Obsolete("This is a Frame Function")]
        public void OnPanelClose(SPanel panel)
        {
            panels.Remove(panel);
            panel.Dispose();
        }
    }
}