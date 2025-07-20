#multi roblox
import threading
import mmap
import time
import psutil
from datetime import datetime

class RobloxInstanceManager:
    def __init__(self):
        self.mutex_name = "ROBLOX_singletonMutex"
        self.shm = mmap.mmap(-1, len(self.mutex_name) + 1, tagname=self.mutex_name, access=mmap.ACCESS_WRITE)
        self.running = True
        self.monitored_pids = {}

    def watch_data(self):
        print("Watching Roblox data...")
        while self.running:
            time.sleep(2)

    def monitor_instances(self):
        print("Waiting for instances...")
        while self.running:
            current_pids = {}
            for proc in psutil.process_iter(attrs=["pid", "name", "cpu_percent", "memory_info", "create_time"]):
                try:
                    if proc.info["name"] == "RobloxPlayerBeta.exe":
                        pid = proc.info["pid"]
                        if pid not in self.monitored_pids:
                            start_time = datetime.fromtimestamp(proc.info["create_time"]).strftime("%Y-%m-%d %H:%M:%S")
                            cpu_usage = proc.info["cpu_percent"]
                            memory_usage = proc.info["memory_info"].rss / (1024 * 1024)
                            print(f"Instance added - PID: {pid}, Start Time: {start_time}, CPU: {cpu_usage}%, Memory: {memory_usage:.2f} MB")
                            self.monitored_pids[pid] = proc.info
                        current_pids[pid] = proc.info
                except (psutil.NoSuchProcess, psutil.AccessDenied):
                    continue

            closed_pids = set(self.monitored_pids) - set(current_pids)
            for pid in closed_pids:
                closed_info = self.monitored_pids[pid]
                print(f"Instance closed - PID: {pid}, Start Time: {datetime.fromtimestamp(closed_info['create_time']).strftime('%Y-%m-%d %H:%M:%S')}")
                del self.monitored_pids[pid]
            
            time.sleep(1)

    def start(self):
        self.watch_thread = threading.Thread(target=self.watch_data)
        self.monitor_thread = threading.Thread(target=self.monitor_instances)
        self.watch_thread.start()
        self.monitor_thread.start()

    def stop(self):
        self.running = False
        self.watch_thread.join()
        self.monitor_thread.join()

manager = RobloxInstanceManager()
manager.start()